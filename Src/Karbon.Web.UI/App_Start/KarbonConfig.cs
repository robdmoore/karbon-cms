namespace Karbon.Web
{
    public class KarbonConfig
    {
        public static void Init()
        {
            new WebBootManager()
                .Initialize();
        }
    }
}