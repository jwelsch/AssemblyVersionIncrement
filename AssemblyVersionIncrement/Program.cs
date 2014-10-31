using System;
using CommandLineLib;

namespace AssemblyVersionIncrement
{
   class Program
   {
      static void Main( string[] args )
      {
         CommandLine<CommandLineArguments> commandLine = null;

         try
         {
            commandLine = new CommandLine<CommandLineArguments>();
            var arguments = commandLine.Parse( args );

            var incrementer = new Incrementer( arguments.AssemblyPath, arguments.ZeroLower );

            switch ( arguments.VersionPart )
            {
               case VersionPart.Major:
               {
                  incrementer.IncrementMajor();
                  break;
               }
               case VersionPart.Minor:
               {
                  incrementer.IncrementMinor();
                  break;
               }
               case VersionPart.Maintenance:
               {
                  incrementer.IncrementMaintenance();
                  break;
               }
               case VersionPart.Build:
               {
                  incrementer.IncrementBuild();
                  break;
               }
               default:
               {
                  throw new ArgumentException( String.Format( "Unknown version component specified: \"{0}\".", arguments.VersionPart ) );
               }
            }

            incrementer.Save();
         }
         catch ( Exception ex )
         {
            if ( commandLine != null )
            {
               Console.WriteLine( commandLine.Help() );
            }

            System.Diagnostics.Trace.WriteLine( ex );
            Console.WriteLine( ex );
         }
      }
   }
}
