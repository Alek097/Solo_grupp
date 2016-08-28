namespace API.Logging
{

	#region Using
	using System;
	#endregion
	public interface ILogger
	{
		void WriteInformation(string message);
		void WriteInformation(Exception ex, string message);
		void WriteWarning(string message);
		void WriteWarning(Exception ex, string message);
		void WriteError(string message);
		void WriteError(Exception ex, string message);
		void WriteFatal(string message);
		void WriteFatal(Exception ex, string message);
		void WriteVerbose(string message);
		void WriteVerbose(Exception ex, string message);
		void WriteDebug(string message);
		void WriteDebug(Exception ex, string message);
	}
}