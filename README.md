# Custom preview tool for DLL files

Encouraged by your feedback in UserVoice asking to add previews to dll files: https://plasticscm.uservoice.com/forums/15467-general/suggestions/17239658-show-assembly-version-in-diff-of-assemblies we would like to show you how to support the scenario by creating your own preview tool.

![alt tag](https://github.com/PlasticSCM/PlasticSCMDllPreview/blob/master/images/initial-screenshot.png)

# What is a custom preview tool

The custom preview tool we are going to build will provide information to the Plastic SCM items and diff views. We will not only generate interesting data based on the “DLL” files but also a nice thumbnail image. As you may know from our previous blogposts ([http://blog.plasticscm.com/2012/03/custom-binary-preview.html] and [http://blog.plasticscm.com/2012/03/binaries-preview-for-game-developers.html]) a preview tool is in charge of providing three different things:
  * Create a small thumbnail of the file.
  * Create a full size image of the file.
  * Create a properties summary of the file.

We’ll create a single tool to handle the three requirements but I just want to let you know you can create one for the thumbnail and the full size images creation and another tool for the properties generation. This can be configured inside the “Plastic SCM Preferences -> Preview Tools -> Add” panel.
Items View
When the file is clicked at the Plastic SCM items view, Plastic will execute our preview tool twice, the first time providing two parameters:
  * The source file path of the file we’ll use for generating the preview image (Thumbnail).
  * The output file path we’ll use in order to write the preview image generated.
The second time Plastic runs the tool to get the properties of the clicked file. To do this, Plastic will only send one argument, the source file path of the clicked file. We need to read the file, generate a text string of data based on the file and write it on the standard output, Plastic will read the preview tool output and the info will be displayed.

## Diff View

When you run a file diff operation inside Plastic and this file has a Preview tool attached, Plastic will request the tool a full-size image of the file and, again, the properties of the file. This is done exactly in the same way as it’s explained for the Items View, the tool is executed twice and using the same input parameters.

## DLL Info

For our Preview tool, we are going to use the windows API in order to get the DLL thumbnail and full-size images, you can check how it’s done by reviewing the “WindowsThumbnailProvider” class. Based on the file extension, the class is able to request the OS a thumbnail for it.

On the other hand, the DLL file properties are calculated inside the “DLLInfoProvider” class, you’ll see how the “System.Diagnostics.FileVersionInfo” is used to read the we DLL FileName, ProductName and Version properties. Check all you can get from it here [https://msdn.microsoft.com/es-es/library/system.diagnostics.fileversioninfo(v=vs.110).aspx] and feel free to improve it.

## Configuration

Open the Plastic SCM preferences, click on “Preview Tools” and add a new one. The “Full path to the executable” has to be just that! The path to the “.exe” we have created. The “Command options to generate a thumbnail preview” field has to match the following: --thumbnail "@src" "@output" and the “Command options to generate a full size preview”: "@src" "@output"

We are using the same tool for both the preview generator and the properties generator so the “Full path to the properties generator” is exactly the same as the one above for the preview tool meanwhile the “Command options to generate file properties” files should just be "@src". That’s it!

For the avoidance of doubt here you have an screenshot of my config:
![alt tag](https://github.com/PlasticSCM/PlasticSCMDllPreview/blob/master/images/config.png)

Check out the results!

This is how the items view looks like:
![alt tag](https://github.com/PlasticSCM/PlasticSCMDllPreview/blob/master/images/itemsView.png)

And the diff view:
![alt tag](https://github.com/PlasticSCM/PlasticSCMDllPreview/blob/master/images/diffdll.png)
