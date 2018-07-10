# image-by-date
Image By Date, a simple tool to divide media files into subdirectories based on date

Main purpose is to get a directory structure for large volumes of eg. image files that you want to organize according to when the image was taken.

Basic usage:

* Type in or select source directory
* Type in or select target directory
* Press button GO! to start process.
* Press button Reset to clear output window and progress bar if you want to continue with another directory.

The program will determine the modified date of each file in the source directory, create applicable subdirectories and move/copy the file to that directory.

Advanced usage:

* [Move/Copy] Check radiobuttons if files should be moved or copied to the target directory
* [Base for date information] Check radiobuttons to select if directories should be created based on the Modified date of the file, or if directories should be created depending on a date in the filename. Usable if you have many files with an incorrect modified date and therefor ends up in another directory than the filename would suggest. Uses regular expressions to determine the date in the filename.
* [Folder creation settings] Check checkboxes to select which directory structure should be created.
    * Should a year directory be created? Will create a structure similar to "C:\MyImages\2018\MyImageFile.jpg"
    * Should a month directory be created? Will create a structure similar to "C:\MyImages\2018\05\MyImageFile.jpg"
    * Should the month name be used instead of the month number? Will create a structure similar to "C:\MyImages\2018\May\MyImageFile.jpg"
    * Should a day directory be created? Will create a structure similar to "C:\MyImages\2018\05\12\MyImageFile.jpg"
    * Should the full date be included in the day directory? Will create a structure similar to "C:\MyImages\2018\05\20180512\MyImageFile.jpg"
    * The above can be combined depending on needs, ie. year and full date to organize in a top level year directory with the exakt date under it. Will create a structure similar to "C:\MyImages\2018\20180512\MyImageFile.jpg"
