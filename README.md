# TuneAssistant
### Quick way to add tags to your local music files
TuneAssistant is a command-line tool used to quickly add tags to your local music files.
It supports mp3, wav, flac, mka, mkv, ogg, wma, aif, aifc and aiff files by default.

# Usage
```
TuneAssistant.exe [options]

/p:{path}           PATH            Path to the folder containing files (required)
/al:{albumname}     ALBUMNAME       Add the album name
/ar:{artistname}    ARTISTNAME      Set the artist name
/ca:{coverart}      COVERART        Set the cover art (supports .jpg .jpeg .png .jfif and .webp files)
/st:{true/ren}      SETTITLE        Set the title based on the filename (use rn with /nt if you want to get rid off numbers)
/nt:{true}          NUMBERTRACKS    Set number tracks based on the filename (example: `01 Test.mp3` -> Track #1)
/yr:{year}          YEAR            Set the release year
/gr:{genre}         GENRE           Set the genre
```

# Examples
### Add the album name and artist
```
TuneAssistant.exe /p:"C:\Users\User\Desktop\cool album" /al:"Rad Tunes" /ar:"Timmy"
```
### Add a cover art
```
TuneAssistant.exe /p:"C:\Users\User\Desktop\cool album" /ca:"C:\Users\User\Desktop\cool album\cover.png"
```

# Attributions
Tagging made possible by [tagLib-Sharp](https://github.com/mono/taglib-sharp)
