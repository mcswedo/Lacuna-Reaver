The script LRE.iss is an Inno Setup script; it builds
the setup program that installs Lacuna Rever to Windows
PCs.  To run the script, you need to install Inno Setup,
which you can get from the following url:

    http://www.jrsoftware.org/isdl.php



Uses( COntent of the LR solution , exe of Lacuna Reaver ,XNA REDIST ,XNAfx4.msi file, dxsetup dotnetfx.exe)  
Able to run script and install the setup. The program will run if the computer already has the prerequisites but more needs to be done as far as checking and installing the prerequisites on computers that don't have them. 

Change Content, Release and OutputDir to local versions.

In the files section, each file/folder needs to be accounted for. Since more folders are being created, the script must be updated as well. This part is easy. 