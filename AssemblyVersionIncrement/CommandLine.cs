using System;
using System.IO;

namespace AssemblyVersionIncrement
{
   /// <summary>
   /// Represents the command line.
   /// </summary>
   internal class CommandLine
   {
      /// <summary>
      /// Gets the version part to increment.
      /// </summary>
      public VersionPart VersionPart
      {
         get;
         private set;
      }

      /// <summary>
      /// Gets the path to the assembly file.
      /// </summary>
      public string AssemblyPath
      {
         get;
         private set;
      }

      ///<summary>
      /// Creates an object of type CommandLine.
      /// </summary>
      /// <param name="args">Raw command line arguments.</param>
      public CommandLine( string[] args )
      {
         this.Parse( args );
      }

      /// <summary>
      /// Returns string with help text.
      /// </summary>
      /// <returns>Text explaining command line usage.</returns>
      public static string Help()
      {
         return
@"Command line usage:
  AssemblyVersionIncrement.exe Major|Minor|Maintenance|Build path-to-assembly-version-file

First argument indicates what part of the version to increment.
Second argument is the path to the assembly version info file.  Example ""$(ProjectDir)Properties\AssemblyInfo.cs""
";
      }

      /// <summary>
      /// Parses the command line arguments.
      /// </summary>
      /// <param name="args">Raw command line arguments.</param>
      private void Parse( string[] args )
      {
         if ( args.Length > 0 )
         {
            try
            {
               this.VersionPart = (VersionPart) Enum.Parse( typeof( VersionPart ), args[0], true );
            }
            catch ( Exception ex )
            {
               throw new CommandLineException( String.Format( "There was an error determining the version part to increment." ), ex );
            }
         }

         if ( args.Length > 1 )
         {
            var exists = false;

            try
            {
               exists = File.Exists( args[1] );
            }
            catch ( Exception ex )
            {
               throw new CommandLineException( String.Format( "There was an error checking for the assembly file at \"{0}\".", args[1] ), ex );
            }

            if ( !exists )
            {
               throw new CommandLineException( String.Format( "The assembly file does not exist at \"{0}\".", args[1] ) );
            }

            this.AssemblyPath = args[1];
         }

         if ( args.Length > 2 )
         {
            throw new CommandLineException( "Too many arguments." );
         }
      }
   }
}
