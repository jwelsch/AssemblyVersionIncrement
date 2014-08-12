using System;

namespace AssemblyVersionIncrement
{
   class Program
   {
      static void Main( string[] args )
      {
         try
         {
            var commandLine = new CommandLine( args );

            var incrementer = new Incrementer( commandLine.AssemblyPath );

            switch ( commandLine.VersionPart )
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
                  throw new ArgumentException( String.Format( "Unknown version component specified: \"{0}\".", commandLine.VersionPart ) );
               }
            }

            incrementer.Save();
         }
         catch ( Exception ex )
         {
            System.Diagnostics.Trace.WriteLine( ex );
            Console.WriteLine( ex );
         }
      }
   }
}
