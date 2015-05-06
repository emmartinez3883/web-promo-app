Imports Microsoft.VisualBasic
Imports System
Imports System.Diagnostics
Imports System.Configuration

Public Class LogIt

    Public Shared Sub LogError(ByRef ErrorMessage As String, ByVal Page As String, ByVal Location As String)

        Dim cd As EventSourceCreationData = New EventSourceCreationData("PRISM", "Application")
        cd.MachineName = "."


        If Not EventLog.SourceExists(cd.Source, cd.MachineName) Then
            EventLog.CreateEventSource(cd)
        End If
        Dim err As String
        err = "Page: " & Page & vbCrLf
        err &= "Location: " & Location & vbCrLf
        err &= "Message: " & ErrorMessage
        Dim ELog As New EventLog(cd.LogName, cd.MachineName, cd.Source)
        ELog.WriteEntry(err)
        'ELog.WriteEntry(sEvent, EventLogEntryType.Warning, 234, CType(3, Short))

    End Sub

End Class
