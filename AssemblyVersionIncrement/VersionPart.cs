using System;

namespace AssemblyVersionIncrement
{
   /// <summary>
   /// Describes a part of a version.
   /// </summary>
   internal enum VersionPart
   {
      /// <summary>
      /// Major version number.
      /// </summary>
      Major,

      /// <summary>
      /// Minor version number.
      /// </summary>
      Minor,

      /// <summary>
      /// Maintenance version number.
      /// </summary>
      Maintenance,

      /// <summary>
      /// Build version number.
      /// </summary>
      Build
   }
}