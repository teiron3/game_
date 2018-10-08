cat -Encoding UTF8 head.txt |Out-File document.xml -Encoding UTF8
cat -Encoding UTF8 ..\doc.xml|Select-Object -Skip 1 |Out-File -Append document.xml -Encoding UTF8