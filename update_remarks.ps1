$root = 'Löwen.Presentation.Api/Controllers/v1'
$files = Get-ChildItem -Path $root -Recurse -Filter *.cs

foreach ($file in $files) {
    $lines = [System.IO.File]::ReadAllLines($file.FullName)
    if ($lines.Length -eq 0) { continue }

    $newLines = New-Object System.Collections.Generic.List[string]
    $i = 0
    $modified = $false

    while ($i -lt $lines.Length) {
        $line = $lines[$i]

        if ($line -match '^(?<indent>\s{8,})///\s*<summary>') {
            $indent = $Matches['indent']
            $newLines.Add($line)
            $i++

            $summaryTexts = @()
            while ($i -lt $lines.Length) {
                $current = $lines[$i]
                $newLines.Add($current)

                if ($current.TrimStart() -match '^///\s*</summary>') {
                    $i++
                    break
                }

                $content = $current.Trim()
                if ($content.StartsWith('///')) {
                    $body = $content.Substring(3).TrimStart()
                    if ($body.Length -gt 0) {
                        $summaryTexts += $body
                    }
                }
                $i++
            }

            $whitespaceBetween = @()
            while ($i -lt $lines.Length -and [string]::IsNullOrWhiteSpace($lines[$i])) {
                $whitespaceBetween += $lines[$i]
                $i++
            }

            $whitespaceAfterRemark = $whitespaceBetween
            if ($i -lt $lines.Length -and $lines[$i].TrimStart().StartsWith('/// <remarks>')) {
                $modified = $true
                $i++
                while ($i -lt $lines.Length -and -not $lines[$i].TrimStart().StartsWith('/// </remarks>')) {
                    $i++
                }
                if ($i -lt $lines.Length) {
                    $i++
                }
                $whitespaceAfterRemark = @()
                while ($i -lt $lines.Length -and [string]::IsNullOrWhiteSpace($lines[$i])) {
                    $whitespaceAfterRemark += $lines[$i]
                    $i++
                }
            } else {
                $modified = $true
            }

            $shortText = 'See summary.'
            foreach ($text in $summaryTexts) {
                $candidate = $text.Trim()
                if ($candidate.Length -gt 0) {
                    $shortText = $candidate
                    break
                }
            }
            if ($shortText.EndsWith('.')) {
                $shortText = $shortText.Substring(0, $shortText.Length - 1)
            }

            $newLines.Add("$indent/// <remarks>")
            $newLines.Add("$indent/// Short usage: $shortText.")
            $newLines.Add("$indent/// </remarks>")
            foreach ($ws in $whitespaceAfterRemark) {
                $newLines.Add($ws)
            }

            continue
        }

        $newLines.Add($line)
        $i++
    }

    if ($modified) {
        [System.IO.File]::WriteAllLines($file.FullName, $newLines)
    }
}

