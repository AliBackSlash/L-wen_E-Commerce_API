import re
from pathlib import Path

root = Path("Löwen.Presentation.Api/Controllers/v1")
files = sorted(root.rglob("*.cs"))

for path in files:
    text = path.read_text(encoding="utf-8")
    lines = text.splitlines(keepends=True)
    if not lines:
        continue

    newline = "\r\n" if any(line.endswith("\r\n") for line in lines) else "\n"
    new_lines: list[str] = []
    i = 0
    total = len(lines)
    modified = False

    while i < total:
        line = lines[i]
        match = re.match(r"(\s{8,})///\s*<summary>", line)
        if not match:
            new_lines.append(line)
            i += 1
            continue

        indent = match.group(1)
        new_lines.append(line)
        i += 1

        summary_texts: list[str] = []
        while i < total:
            current = lines[i]
            new_lines.append(current)

            if re.match(rf"{re.escape(indent)}///\s*</summary>", current.strip()):
                i += 1
                break

            content = current.strip()
            if content.startswith("///"):
                body = content[3:].lstrip()
                if body:
                    summary_texts.append(body)

            i += 1
        else:
            break

        whitespace_between: list[str] = []
        while i < total and lines[i].strip() == "":
            whitespace_between.append(lines[i])
            i += 1

        whitespace_after_remark: list[str] = []
        idx = i
        if idx < total and lines[idx].lstrip().startswith("/// <remarks>"):
            modified = True
            idx += 1
            while idx < total and not lines[idx].lstrip().startswith("/// </remarks>"):
                idx += 1
            if idx < total:
                idx += 1
            while idx < total and lines[idx].strip() == "":
                whitespace_after_remark.append(lines[idx])
                idx += 1
            i = idx
        else:
            whitespace_after_remark = whitespace_between

        short_text = next((s for s in summary_texts if s), "See summary.")
        short_text = short_text.strip()
        if short_text.endswith("."):
            short_text = short_text[:-1]

        remark_block = [
            f"{indent}/// <remarks>{newline}",
            f"{indent}/// Short usage: {short_text}.{newline}",
            f"{indent}/// </remarks>{newline}",
        ]

        new_lines.extend(remark_block)
        new_lines.extend(whitespace_after_remark)
        modified = True

    if modified:
        path.write_text("".join(new_lines), encoding="utf-8")

