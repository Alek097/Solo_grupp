namespace API.Logging
{
	#region Using
	using System;
	using Serilog.Events;
	using System.Configuration;
	using System.IO;
	using System.Threading;
	using Serilog;
	using System.Data.SqlClient;
	using Serilog.Sinks.MSSqlServer;
	using System.Collections.ObjectModel;
	using System.Data;
	using System.Xml.Serialization;
	#endregion
	public class Logger : ILogger
	{
		private static string _connectString;

		static Logger()
		{

			_connectString = ConfigurationManager.ConnectionStrings["solo_grupp_logs"].ConnectionString;

			try
			{
				SqlConnection connection = new SqlConnection(_connectString);
				connection.Open();
				try
				{
					SqlCommand hasTableCommand = new SqlCommand(@"SELECT 1 from Logs", connection);
					hasTableCommand.ExecuteNonQuery();
				}
				catch (SqlException)
				{
					CreateTable(connection);
				}
				finally
				{
					connection.Close();
					connection.Dispose();
				}
			}
			catch
			{

			}
		}

		private void Write(string message, LogEventLevel level)
		{
			var log = new LoggerConfiguration()
				.WriteTo.MSSqlServer(_connectString, "Logs", columnOptions: GetColumnOptions())
				.CreateLogger();
			log.Write(level, message);
		}
		private void Write(Exception ex, string message, LogEventLevel level)
		{
			var log = new LoggerConfiguration()
				.WriteTo.MSSqlServer(_connectString, "Logs", columnOptions: GetColumnOptions(ex))
				.CreateLogger();

			log.Write(level, ex, message);
		}
		private static void CreateTable(SqlConnection connection)
		{
			string commandText =
				@"CREATE TABLE [dbo].[Logs] (
                [Id]              INT                IDENTITY (1, 1) NOT NULL,
                [Message]         NVARCHAR (MAX)     NULL,
                [MessageTemplate] NVARCHAR (MAX)     NULL,
                [Level]           NVARCHAR (128)     NULL,
                [TimeStamp]       DATETIMEOFFSET (7) NOT NULL,
                [Exception]       NVARCHAR (MAX)     NULL,
                [Properties]      XML                NULL,
                [ThreadId]        INT                NULL,
                [StackTrace]      NVARCHAR (MAX)     NULL,
                [InnerExceptions] XML                NULL,
                CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
                );";

			SqlCommand commandCreate = new SqlCommand(commandText, connection);
			commandCreate.ExecuteNonQuery();
		}
		private static ColumnOptions GetColumnOptions(Exception ex = null)
		{

			return new ColumnOptions
			{
				AdditionalDataColumns = new Collection<DataColumn>
				{
					new DataColumn {DataType = typeof (int), ColumnName = "ThreadId", DefaultValue = Thread.CurrentThread.ManagedThreadId},
					new DataColumn {DataType = typeof(string), ColumnName = "StackTrace", DefaultValue = Environment.StackTrace },
					new DataColumn {ColumnName = "InnerExceptions", DefaultValue = ex == null? null : GetInnerExceptions(ex) }
				}
			};
		}
		private static string GetInnerExceptions(Exception ex)
		{
			XmlSerializer serilizer = new XmlSerializer(typeof(ExceptionSerializel));

			using (TextWriter writer = new StringWriter())
			{
				serilizer.Serialize(writer, new ExceptionSerializel(ex));

				return writer.ToString();
			}
		}

		public void WriteInformation(string message)
		{
			this.Write(message, LogEventLevel.Information);
		}

		public void WriteInformation(Exception ex, string message)
		{
			this.Write(ex, message,LogEventLevel.Information);
		}

		public void WriteWarning(string message)
		{
			this.Write(message, LogEventLevel.Warning);
		}

		public void WriteWarning(Exception ex, string message)
		{
			this.Write(ex, message, LogEventLevel.Warning);
		}

		public void WriteError(string message)
		{
			this.Write(message, LogEventLevel.Error);
		}

		public void WriteError(Exception ex, string message)
		{
			this.Write(ex, message, LogEventLevel.Error);
		}

		public void WriteFatal(string message)
		{
			this.Write(message, LogEventLevel.Fatal);
		}

		public void WriteFatal(Exception ex, string message)
		{
			this.Write(ex, message, LogEventLevel.Fatal);
		}

		public void WriteVerbose(string message)
		{
			this.Write(message, LogEventLevel.Verbose);
		}

		public void WriteVerbose(Exception ex, string message)
		{
			this.Write(ex, message, LogEventLevel.Verbose);
		}

		public void WriteDebug(string message)
		{
			this.Write(message, LogEventLevel.Debug);
		}

		public void WriteDebug(Exception ex, string message)
		{
			this.Write(ex, message, LogEventLevel.Debug);
		}
	}
}
