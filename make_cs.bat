C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:test.exe /doc:doc.xml /recurse:*.cs
cd document
powershell ./test.ps1
cd ..
