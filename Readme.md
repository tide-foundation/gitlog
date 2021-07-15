To publish:
dotnet publish -c Release -r win10-x64 -p:PublishSingleFile=true


To run:
Ensure you have a config.txt file with comma seperated repo folder names
gitlog.exe path/to/config/and/output/folder

You can optionally add a 2nd argument to override the command