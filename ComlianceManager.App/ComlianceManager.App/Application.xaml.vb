Imports System.Windows.Threading

Class Application

    Private Sub Application_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        MessageBox.Show("Fehler: " & e.Exception.Message & vbNewLine & vbNewLine & "Bitte melden Sie diese Nachricht den Administrator.")
        e.Handled = True
    End Sub

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        Dim win As New Start
        win.Show()
    End Sub

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

End Class
