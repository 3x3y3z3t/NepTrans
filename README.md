# NepTrans
A tool for supporting the translation of the (series) game Choujigen Game Neptune (Hyperdimension Neptunia).

What this tool do is it presents all the reference text (Japanese and English) for one line 
so you don't have to tab between files for reference, or edit the original file.
Changes will be kept track of and be written to output file with the same format as the original scripts.
Saves is made automatically when program is closing, or manually using a Save button.

Current version: v0.2.5

Supported games:
- Choujijigen Game Neptune Re;Birth1

Features:
- Present all reference text for one line so no need to tab between files.
- Visualize project structure with directory tree.
- Progress tracker that shows translation progress.
- A summary report dialog that shows progress as Pie Chart.

Requirement:
- The tool require you to have both the Japanese script and English script of the game. Otherwise it will throw a bunch of No Data error at you.
- Directory structure is enforced.

To-do list/Future features:
- Supports for the other games in the series.
- Allow loading data from one script only (Japanese or English).
- (Future) Refactor and make this an universal tool for other game's translating as well.
