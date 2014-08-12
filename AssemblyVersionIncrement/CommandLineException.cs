using System;

namespace AssemblyVersionIncrement
{
   /// <summary>
   /// Thrown when there is an exception parsing the command line arguments.
   /// </summary>
   [Serializable]
   internal class CommandLineException : Exception
   {
      /// <summary>
      /// Constructs an object of type CommandLineException.
      /// </summary>
      public CommandLineException()
      {
      }

      /// <summary>
      /// Constructs an object of type CommandLineException.
      /// </summary>
      /// <param name="message">The message associated with the exception.</param>
      public CommandLineException( string message )
         : this( message, null )
      {
      }

      /// <summary>
      /// Constructs an object of type CommandLineException.
      /// </summary>
      /// <param name="message">The message associated with the exception.</param>
      /// <param name="innerException">Another exception associated with the CommandLineException.</param>
      public CommandLineException( string message, Exception innerException )
         : base( message, innerException )
      {
      }

      /// <summary>
      /// Constructs a CommandLineException object.
      /// </summary>
      /// <param name="info">The serialization information.</param>
      /// <param name="context">The streaming context.</param>
      protected CommandLineException( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context )
         : base( info, context )
      {
      }
   }
}
