# Twitch-Chat-Reader
Multi-Threaded Application that will
Read multiple Twitch channel chats, parse the messages and save them to MySQL DB

Designed to read 10k - 25k chat lines per minute, tested at about 350 lines per second. This fully utilised an i7 7700k @ 4.8Ghz (mySQL instance was on same machine)
