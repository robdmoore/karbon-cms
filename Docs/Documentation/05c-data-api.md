# Data API
Most Karbon entities will expose a `.Data` property. Whilst you can access the values directly as you would with any other dictionary, the following  methods also exist to provide you with built in error checking and fallback values.

## Methods

### .GetValue(String key, [String defaultValue]):String
Gets the value for the given key. If no key exists, or the value is empty, the optional defaultValue will be returned.

### .TryGetValue(String key, out String value):Bool
Gets a flag indicating whether a value for the given key can be found.
