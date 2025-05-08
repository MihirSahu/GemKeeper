# GemKeeper
A CLI tool to export Gemini Chats to markdown for archival purposes. Inspired by [ChatKeeper]()
## Usage
```
mihirsahu@Mihirs-MacBook-Pro-3 GemKeeper % ./GemKeeper 
Gemkeeper v1.0.0
Made with <3 by Mihir Sahu

USAGE:
Export Gemini chat history to markdown:
  gemkeeper --file-path ./takeout.zip --output gemini_output/

  -f, --file-path    Required. Specify the activity file path.
  -o, --output       Required. Specify the output folder.
  -v, --verbose      Print additional logs.
  --help             Display this help screen.
  --version          Display version information.
```
## Example
```
mihirsahu@Mihirs-MacBook-Pro-3 GemKeeper % ./bin/Debug/net9.0/GemKeeper -f /Users/mihirsahu/Downloads/takeout-20250504T042039Z-001.zip -o /Users/mihirsahu/Downloads/output/
```
```
output/
├── 2025-04-30-31.md
├── 2025-04-30-37.md
├── 2025-04-30-41.md
├── 2025-04-30-47.md
├── 2025-04-30-51.md
├── 2025-04-30-53.md
├── 2025-04-30-54.md
├── 2025-04-30-59.md
├── 2025-05-02-18.md
├── 2025-05-02-19.md
├── 2025-05-02-20.md
├── 2025-05-02-28.md
├── 2025-05-02-44.md
├── 2025-05-02-57.md
└── 2025-05-03-45.md
```
## TODO
- Implement verbose functionality
- Add folders aggregated by year/month for organizational purposes
- Create binaries for releases