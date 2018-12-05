$content = (Get-Content ".\Day01.txt" -Raw).Split([System.Environment]::NewLine)
$content | ForEach-Object -Begin { $sum = 0 } -Process { $sum += [int]$_ } -end { "Total is $sum" }