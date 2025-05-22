# GemKeeper
A CLI tool to export Gemini Chats to markdown for archival purposes. Inspired by [ChatKeeper](https://martiansoftware.com/chatkeeper/).
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
mihirsahu@Mihirs-MacBook-Pro-3 net9.0 % ./GemKeeper -f /Users/mihirsahu/Downloads/takeout-20250521T194916Z-1-001.zip -o /Users/mihirsahu/Downloads/output/ -a -v
```
```
Google Takeout file path: /Users/mihirsahu/Downloads/takeout-20250521T194916Z-1-001.zip
Output path: /Users/mihirsahu/Downloads/output/

The extraction process was successful.
MyActivity.json data deserialized successfully.
Prompts processed successfully.

2025-04-28-32 ✓
2025-04-28-08 ✓
2025-04-30-28 ✓
2025-05-01-47 ✓
2025-05-01-54 ✓
2025-05-02-18 ✓
2025-05-02-44 ✓
2025-05-03-28 ✓
2025-05-04-45 ✓
2025-05-04-47 ✓
2025-05-07-10 ✓
2025-05-08-14 ✓
2025-05-10-52 ✓
2025-05-10-56 ✓
2025-05-17-10 ✓
2025-05-18-34 ✓
2025-05-19-05 ✓
2025-05-19-07 ✓
2025-05-19-31 ✓
2025-05-19-51 ✓
73 activities processed.
Chat history exported to /Users/mihirsahu/Downloads/output/.
```