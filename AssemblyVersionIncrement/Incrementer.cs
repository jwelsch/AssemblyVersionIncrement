using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AssemblyVersionIncrement
{
   /// <summary>
   /// Increments the version of an assembly.
   /// </summary>
   internal class Incrementer
   {
      /// <summary>
      /// Path to the assembly info file to increment.
      /// </summary>
      private string assemblyInfoPath;

      /// <summary>
      /// Indicates whether or not to zero out lower order version numbers.
      /// </summary>
      private bool zeroLower;

      /// <summary>
      /// Regex used to pull out assembly version information.
      /// </summary>
      private Regex versionRegex = new Regex( @"\[ ?assembly ?: ?AssemblyVersion ?\( ?""[\d]+(\.[\d]+(\.[\d]+(\.[\d]+)?)?)?"" ?\) ?\]" );

      /// <summary>
      /// Regex used to pull out assembly file version information.
      /// </summary>
      private Regex fileVersionRegex = new Regex( @"\[ ?assembly ?: ?AssemblyFileVersion ?\( ?""[\d]+(\.[\d]+(\.[\d]+(\.[\d]+)?)?)?"" ?\) ?\]" );

      private string versionString = @"[assembly: AssemblyVersion( ""{0}.{1}.{2}.{3}"" )]";
      private string fileVersionString = @"[assembly: AssemblyFileVersion( ""{0}.{1}.{2}.{3}"" )]";

      /// <summary>
      /// Gets the major version.
      /// </summary>
      public int Major
      {
         get;
         private set;
      }

      /// <summary>
      /// Gets the minor version.
      /// </summary>
      public int Minor
      {
         get;
         private set;
      }

      /// <summary>
      /// Gets the maintenance version.
      /// </summary>
      public int Maintenance
      {
         get;
         private set;
      }

      /// <summary>
      /// Gets the build number.
      /// </summary>
      public int Build
      {
         get;
         private set;
      }

      /// <summary>
      /// Creates an object of type Incrementer.
      /// </summary>
      /// <param name="assemblyInfoPath">Path to the assembly info file to increment.</param>
      /// <param name="zeroLower">True to zero out lower order version, false to leave.</param>
      public Incrementer( string assemblyInfoPath, bool zeroLower )
      {
         this.assemblyInfoPath = assemblyInfoPath;
         this.zeroLower = zeroLower;
         this.LoadAssemblyInfo();
      }

      /// <summary>
      /// Loads the assembly information.
      /// </summary>
      private void LoadAssemblyInfo()
      {
         var text = String.Empty;

         using ( var stream = new StreamReader( this.assemblyInfoPath ) )
         {
            text = stream.ReadToEnd();
         }

         var match = this.versionRegex.Match( text );

         if ( match.Success )
         {
            this.GetVersionDigits( match );
         }
         else
         {
            throw new Exception( "Failed to find assembly version." );
         }

         match = this.fileVersionRegex.Match( text );

         if ( match.Success )
         {
            this.GetVersionDigits( match );
         }
         else
         {
            throw new Exception( "Failed to find assembly file version." );
         }
      }

      /// <summary>
      /// Gets the version digits from a regex match of information in the assembly info file.
      /// </summary>
      /// <param name="regexMatch">Regex match of version information.</param>
      private void GetVersionDigits( Match regexMatch )
      {
         var numeralsRegex = new Regex( @"[\d]+(\.[\d]+(\.[\d]+(\.[\d]+)?)?)?" );

         var numerals = numeralsRegex.Match( regexMatch.Value );
         var digits = numerals.Value.Split( '.' );

         this.Major = Int32.Parse( digits[0] );

         if ( digits.Length > 1 )
         {
            this.Minor = Int32.Parse( digits[1] );
         }
         else
         {
            this.Minor = 0;
         }

         if ( digits.Length > 2 )
         {
            this.Maintenance = Int32.Parse( digits[2] );
         }
         else
         {
            this.Maintenance = 0;
         }

         if ( digits.Length > 3 )
         {
            this.Build = Int32.Parse( digits[3] );
         }
         else
         {
            this.Build = 0;
         }
      }

      /// <summary>
      /// Saves the new version information to the assembly info file.
      /// </summary>
      public void Save()
      {
         var text = String.Empty;

         using ( var stream = new StreamReader( this.assemblyInfoPath ) )
         {
            text = stream.ReadToEnd();
         }

         text = this.versionRegex.Replace( text, String.Format( this.versionString, this.Major, this.Minor, this.Maintenance, this.Build ) );
         text = this.fileVersionRegex.Replace( text, String.Format( this.fileVersionString, this.Major, this.Minor, this.Maintenance, this.Build ) );

         using ( var stream = new StreamWriter( this.assemblyInfoPath ) )
         {
            stream.Write( text );
         }
      }

      /// <summary>
      /// Increments the major version.  Sets all lower version numbers to zero.
      /// </summary>
      public void IncrementMajor()
      {
         this.Major++;

         if ( this.zeroLower )
         {
            this.Minor = 0;
            this.Maintenance = 0;
            this.Build = 0;
         }
      }

      /// <summary>
      /// Increments the minor version.  Does not change any upper version numbers.  Sets all lower version numbers to zero.
      /// </summary>
      public void IncrementMinor()
      {
         this.Minor++;

         if ( this.zeroLower )
         {
            this.Maintenance = 0;
            this.Build = 0;
         }
      }

      /// <summary>
      /// Increments the maintenance version.  Does not change any upper version numbers.  Sets all lower version numbers to zero.
      /// </summary>
      public void IncrementMaintenance()
      {
         this.Maintenance++;

         if ( this.zeroLower )
         {
            this.Build = 0;
         }
      }

      /// <summary>
      /// Increments the build number.  Does not change any upper version numbers.
      /// </summary>
      public void IncrementBuild()
      {
         this.Build++;
      }
   }
}
