RMDIR /Q /S dist
MKDIR dist
MKDIR dist\files-core
MKDIR dist\files-fpml
XCOPY /S files-core dist\files-core
XCOPY /S files-fpml dist\files-fpml
COPY Validate\bin\Release\*.dll dist
COPY Validate\bin\Release\*.exe dist
COPY misc-fpml\*.* dist
COPY license.txt dist