# WWTBM
This program Is meant to recreate the game of "Who Wants To Be A Millionaire" but with "audio" and "Image" Questions.

The design is Very simple. questions can be easily added directly in gamefiles >>> see edit section


## Usage
compile open exe and play.

You may find some questions already created, feel free to remove them or translate them in your langauge.

in game: Press "M" to play/stop sound


## Create/Edit Questions

To create questions we simply modify the "Questions.txt" file witch you can find in "data/XXXXQuestions/Questions.txt"

## Question File
A question file is "new line" sensitive. Every Questions is 6 lines long:

0 - QuestionNumber) Question Title
1 - Question answer 1
2 - Question answer 1  <-- 2
3 - Question answer 1
4 - Question answer 1
5 - Answer Number      --> 2

### Every questions needs a number and a parenthesis
The number indicates the type of questions
1-299      normal questions
300 - 599  Audio
600+       Images

### For every special questions there must be a specific file
Every image question needs a .PNG image (should automatic resize)
Every audio question needs a Audio.MP3 Files

## To reset Questions Go to Settings in the main menù
For every question ANSWERED it gets balcklisted, to reset just click the reset button in settings


# A Working compiled version is present in bin/debug
