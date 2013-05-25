# Media API

## Properties

Media items have the following predefined properties.

### .Name:String
Gets the name of the page parsed from the folder name. This can be overridden by declaring a `Name` variable in the content file.

### .Slug:String
Gets the URL name for the file.

### .Extension:String
The file extension of the file. 

### .Size:Long
The size of the file in bytes.

### .TypeName:String
Gets the type name for the file as parsed from the file name.

### .SortOrder:Int
Gets the sort order for the file.

### .RelativePath:String
Gets the path of the file relative to the content store root.

### .RelativeUrl:String
Gets the URL of the file relative to the application root.

### .ContentRelativeUrl:String
Gets the relative URL of the associated content item.

### .Created:DateTimeOffset
Gets the creation date of the file.

### .Modified:DateTimeOffset
Gets the last modified date of the file.

### .Data:IDictionary<String, String>
Gets the key value dictionary of parsed content from the meta data file. 

Whilst you can access the values of the dictionary directly, it is advised to use the `.Get(...)` methods instead, as they offer some in-built error handling and fallback values for a better experience.

## Methods

### .Url():String
Get the absolute URL for the content.

### .NiceSize():String
Gets the size of the file formatted in a human readable format.

### .MimeType():String
Gets the mime type of the file. 

### .IsImage():Bool
Gets a flag indicating whether the file is an image. 
	
### .IsVideo():Bool
Gets a flag indicating whether the file is a video.

### .IsSound():Bool
Gets a flag indicating whether the file is a sound.

### .IsDocument():Bool 
Gets a flag indicating whether the file is a document.

### .Get(String key, [String defaultValue]):String
Gets the value for the given key from the Data collection. If no key exists, or the value is empty, the optional defaultValue will be returned.

### .TryGet(String key, out String value):Bool
Gets a flag indicating whether a value for the given key can be found in the Data collection.

## Traversal

### .HasPrev([Func<IFile, Bool> filter]):Bool
Gets a flag indicating whether the file has a previous sibling optionally filtered by the filter function parameter.

### .Prev([Func<IFile, Bool> filter]): IFile
Gets the previous file optionally filtered by the filter function parameter. 

### .HasNext([Func<IFile, Bool> filter]):Bool
Gets a flag indicating whether the file has a next sibling
optionally filtered by the filter function parameter.

### .Next([Func<IFile, Bool> filter]): IFile
Gets the previous file optionally filtered by the filter function parameter. 

## Images
In addition to the above, image files also have the following additional properties and methods.

## Properties

### .Width:Int
Gets the width of the image.

### .Height:Int
Gets the height of the image.

## Methods
The following “Fit” methods all make use of the ImageResizing.net library. See [http://imageresizing.net/docs/basics](http://imageresizing.net/docs/basics) for details of advanced options.

All “Fit” methods return an IFilteredImage object to allow you to daisy chain additional commands together. To get the final URL, simply output the object itself or explicitly call `.Url()`.

### .FitWidth(int width):IFilteredImage
Gets the URL of the image resized to fit within the supplied with parameter. 

### .FitHeight(int height):IFilteredImage
Gets the URL of the image resized to fit within the supplied height parameter. 

### .Fit(int maxWidthHeight, [FitMode fitMode, ScaleMode scaleMode, AlignMode alignMode, ImageFormat format, int quality, int colors, string bgColor]):IFilteredImage
### .Fit(int width, int height, [FitMode fitMode, ScaleMode scaleMode, AlignMode alignMode, ImageFormat format, int quality, int colors, string bgColor]):IFilteredImage
Gets the URL of the image resized to fit within the supplied width / height parameter, optionally constrained by the supplied parameters.
