# WPI
An console application that allows you to copy current Windows 10 Preview Images.

How to use:  
1. Execute bin/Release/WindowsPreviewImages.exe  
2a. If there was no errors click any key. After that a folder containing images should open; if it didn't happen, default save location is D:/preview/year-month-day-hour-minute-second with DateTime part of execution time.  
  
Exceptions:  
2b. "Source directory does not exist; if you used default settings you'll have to add new Windows Preview Images path as a first parameter of this program." - goto 3.  
2c. "An exception occured while copying files: {exception message}" - goto 4.  
2d. "Destination folder does not exist anymore; can't set file names." - goto 5.  
2e. "An exception occured while setting file names: {exception message}" - goto 6.  
3. You need to provide new Windows Image Preview source as a first execution parameter. Use Google and modify file settings.  
4. Something prevented program from copying files - out of disk space is most likely scenario, other are: unnatural system state, disk error or antivirus. Try restarting program or restarting computer. Do not whitelist my program; I hold no responsibility for any damage done by virus that could potentially modify program executable.  
5. Something actively removed output folder after files were copied but before program was able to set proper filenames; it's likely either antivirus or virus.  
6. Only exception message could provide you with information what happened; note that files can be intact. Changing their file type to jpg will propably open them.  
  
Advanced:  
Program can be alternatively run in advanced mode, by providing one or two arguments:  
First argument - destination path (default: "D:\\\\preview\\\\" - current DateTime will be used as a output folder).  
Second argument - source path.  
  
Remember that if source changed you HAVE TO provide any valid output path as a first parameter.  
  
# Important  
I do not guarantee *anything* - you have source code, you can go and check it. Compile it for yourself. Do whatever you want.  
As a original author I don't mind you using my code ;) But it would be very nice to add any credit like "Inspired by Hekkaryk" or something like that in your work. Thanks! ^_^
