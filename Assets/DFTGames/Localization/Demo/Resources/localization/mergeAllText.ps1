$utf8NoBom = New-Object System.Text.UTF8Encoding($false)  # $false => no BOM
$outPath   = "AllText.txt"

$sw = New-Object System.IO.StreamWriter($outPath, $false, $utf8NoBom)
try {
    Get-ChildItem -Filter *.txt -File | ForEach-Object {
        Get-Content -LiteralPath $_.FullName -Encoding UTF8 | ForEach-Object {
            $sw.WriteLine($_)
        }
    }
}
finally {
    $sw.Dispose()
}
