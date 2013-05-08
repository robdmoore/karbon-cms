using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core.Serialization
{
    public class KarbonDataSerializer : DataSerializer
    {
        private const string KeyTerminator = ":";
        private const string ValueTerminator = "----";

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public override IDictionary<string, string> Deserialize(Stream data)
        {
            // Move stream to start
            if (data.CanSeek)
                data.Seek(0, 0);

            // Setup result holder
            var result = new Dictionary<string, string>();

            // Setup placeholders
            var currentKey = "";
            var currentValue = new StringBuilder();

            // Read stream line by line
            using(var reader = new StreamReader(data))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(string.IsNullOrEmpty(currentKey))
                    {
                        // Look for key
                        var terminatorIndex = line.IndexOf(KeyTerminator, StringComparison.InvariantCulture);
                        if(terminatorIndex > 0)
                        {
                            var possibleKey = line.Substring(0, terminatorIndex);
                            // Strip non valid chard + replace spaces with underscores
                            if(possibleKey.IsAlphaNumeric())
                            {
                                currentKey = possibleKey;
                                if(line.Length > terminatorIndex)
                                {
                                    currentValue.AppendLine(line.Substring(terminatorIndex + 1).Trim());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (line != ValueTerminator)
                        {
                            // Append line to value
                            currentValue.AppendLine(line.Trim());
                        }
                        else
                        {
                            // Add the current value to result
                            if (result.ContainsKey(currentKey))
                                result[currentKey] = currentValue.ToString().Trim();
                            else
                                result.Add(currentKey, currentValue.ToString().Trim());

                            // Reset placeholders
                            currentKey = "";
                            currentValue = new StringBuilder();
                        }
                    }
                }
            }

            // Append any unfinished entries
            if(!string.IsNullOrEmpty(currentKey))
            {
                // Add the current value to result
                if (result.ContainsKey(currentKey))
                    result[currentKey] = currentValue.ToString().Trim();
                else
                    result.Add(currentKey, currentValue.ToString().Trim());
            }

            // Return parsed values
            return result;
        }
    }
}
