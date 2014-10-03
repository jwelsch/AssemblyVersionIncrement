# AssemblyVersionIncrement
Automatically increments the version of an assembly when it is built in Visual Studio.

## How To Use
AssemblyVersionIncrement works on a per-project basis.  So, it must be set up for each project that needs to be incremented.

In the project's "Properties" page (select the project in the Solution Explorer and hit Alt+Enter), go to the "Build Events" tab.  In the "Pre-build event command line" box set up the command line to run AssemblyVersionIncrement.

### Command Line Usage
  path-to-assemblyversionincrement version-part assembly-to-increment

### Command Line Arguments
version-part: Part of the version that will be incremented.  Options are - Major, Minor, Maintenance, Build.
assembly-to-increment: Path to the file containing the version information for the assembly that will have its version incremented.  Example "$(ProjectDir)Properties\AssemblyInfo.cs".
