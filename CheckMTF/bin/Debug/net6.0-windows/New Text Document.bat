Get-ChildItem -force 'C:\Users\TA\OneDrive\Desktop\datatest' -Recurse| ForEach-Object{$_.LastWriteTime = ('01/02/2019 17:10:00')}