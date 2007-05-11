RMDIR /Q /S dist
MKDIR dist
MKDIR dist\files
XCOPY /S files dist\files
COPY Validate\bin\Release\*.dll dist
COPY Validate\bin\Release\*.exe dist
COPY Classify\bin\Release\*.dll dist
COPY Classify\bin\Release\*.exe dist
COPY misc\*.* dist
COPY license.txt dist