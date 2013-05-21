# Content API

## Properties

Aswell as the content key values you define in the content file, content entities also have the following predefined properties.

### .Name:String
Gets the name of the page parsed from the folder name. This can be overridden by declaring a `Name` variable in the content file.

### .Slug:String
Gets the URL name for the page.

### .TypeName:String
Gets the type name for the content as parsed from the content file
name (as used to select the custom template to use).

### .SortOrder:Int
Gets the sort order for the content.

### .Depth:Int
Gets the depth of the content.

### .RelativePath:String
Gets the path of the content relative to the content store root.

### .RelativeUrl:String
Gets the URL of the content relative to the application root.

### .Created:DateTimeOffset
Gets the creation date of the content.

### .Modified:DateTimeOffset
Gets the last modified date of the content.

### .Data:IDictionary<String, String>
Gets the key value dictionary of parsed content from the content file. 

Whilst you can access the values of the dictionary directly, it is advised to use the `.GetValue(...)` methods instead, as they offer some in-built error handling and fallback values for a better experience.

See the [Data API](05c-data-api.md) for further information on the `GetValue(...)` methods.

## Methods

### .Url():String
Get the absolute URL for the content.

### .IsVisible():Bool
Gets a flag indicating whether the content is visible or not.

### .IsOpen():Bool
Gets a flag indicating whether the content is open or not.

### .IsHomePage():Bool
Gets a flag indicating whether the content is the home page.

### .IsChildOf(IContent content):Bool
Gets a flag indicating whether the content is a child of the
passed in content.

### .IsAncestorOf(IContent content):Bool
Gets a flag indicating whether the content is an ancestor of the
passed in content.

### .IsDescendantOf(IContent content):Bool
Gets a flag indicating whether the content is a descendant of the
passed in content.

### .HasPrev([Func<IContent, Bool> filter]):Bool
Gets a flag indicating whether the content has a previous sibling
optionally filtered by the filter function parameter.

### .HasNext([Func<IContent, Bool> filter]):Bool
Gets a flag indicating whether the content has a next sibling
optionally filtered by the filter function parameter.

## Traversal
Once you have a piece of content, it is likely that you may want to find other content based upon it. To do this, you can navigate up and down the site structure using the following methods. 

We've based all the traversal methods on the jQuery syntax, so we hope they will look a little familiar to you.

### .Parent():IContent
Gets the parent content item.

### .Parents([Func<IContent, Bool> filter]):IEnumerable<IContent>
Gets the ancestor content items optionality filtered by the filter function parameter. 

### .Closest(Func<IContent, Bool> filter):IContent
Gets the closest ancestor content item filtered by the filter function parameter. 

### .Children([Func<IContent, Bool> filter]):IEnumerable<IContent>
Gets the child content items optionally filtered by the filter function parameter.

### .Siblings([Func<IContent, Bool> filter]):IEnumerable<IContent>
Gets the sibling content items optionally filtered by the filter function parameter. 

### .Prev([Func<IContent, Bool> filter]): IContent
Gets the previous content item optionally filtered by the filter function parameter. 

### .Next([Func<IContent, Bool> filter]): IContent
Gets the previous content item optionally filtered by the filter function parameter. 

### .Find(Func<IContent, Bool> filter):IEnumerable<IContent>
Gets the descendant content items optionally filtered by the filter function parameter. 

## File Access

To access media files associated with a content item the content item itself has a number of methods to make it really easy.

### .Files([Func<IFile, Bool> filter]):IEnumerable<IFile>
Gets all files optionally filtered by the filter function parameter.

### .Images([Func<IImageFile, Bool> filter]):IEnumerable<IImageFile>
Gets all image files optionally filtered by the filter function parameter.

For files to be recognised as an image file, they must have one of the following file extensions:

- Jpg	
- Jpeg	
- Gif
- Png
- Bmp
- Tif
- Tiff

### .Videos([Func<IVideoFile, Bool> filter]):IEnumerable<IVideoFile>
Gets all video files optionally filtered by the filter function parameter.

For files to be recognised as a video file, they must have one of the following file extensions:

- Ogg
- Ogv
- Webm
- Mp4
- Mov
- Avi
- Flv
- Swf

### .Sounds([Func<ISoundFile, Bool> filter]):IEnumerable<ISoundFile>
Gets all sound files optionally filtered by the filter function parameter.

For files to be recognised as a sound file, they must have one of the following file extensions:

- Mp3
- Wav
- Wma
- Mid
- Ra
- Ram
- Rm

### .Documents([Func<IDocumentFile, Bool> filter]):IEnumerable<IDocumentFile>
Gets all document files optionally filtered by the type and filter function parameter. 

For files to be recognised as a sound file, they must have one of the following file extensions:

- Pdf
- Doc
- Docx
- Xls
- Xlsx
- Ppt
- Pptx
- Rtf

See the Media API section for further information on the properties / methods available to the various supported file types.