using System;
using CommandLineLib;

namespace AssemblyVersionIncrement
{
   internal class CommandLineArguments
   {
      /// <summary>
      /// Gets the version part to increment.
      /// </summary>
      [EnumValue( 1, Description = "What part of the version to increment.  Valid values are Major, Minor, Maintenance, or Build." )]
      public VersionPart VersionPart
      {
         get;
         private set;
      }

      /// <summary>
      /// Gets the path to the assembly file.
      /// </summary>
      [FilePath( 2, Description = "The path to the assembly version info file.  Example \"$(ProjectDir)Properties\\AssemblyInfo.cs\"" )]
      public string AssemblyPath
      {
         get;
         private set;
      }

      [Switch( "-zeroLower", Optional = true, Aliases = new string[] { "-z" }, Description = "Indicates to zero out lower order version numbers when a higher one is incremented." )]
      public bool ZeroLower
      {
         get;
         private set;
      }
   }
}
