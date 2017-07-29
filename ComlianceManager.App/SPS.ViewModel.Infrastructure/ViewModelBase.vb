Imports System.ComponentModel
Imports System.Runtime.CompilerServices



''' <summary>
''' Basisklasse für alle ViewModel-Klassen, jede ViewModel-Klasse sollte von dieser Basisklasse erben.
''' Implementiert INotifyPropertyChanged, INotifyPropertyChanging, IDataErrorInfo und IValidationInfo
''' </summary>
Public MustInherit Class ViewModelBase
    Implements INotifyPropertyChanged, INotifyPropertyChanging, IDisposable

    Private _title As String

    Public Overridable Property VMTitle As String
        Get
            Return _title
        End Get
        Set
            _title = Value
            RaisePropertyChanged("VMTitle")
        End Set
    End Property

    Private _desciption As String

    Public Property VMDescription As String
        Get
            Return _desciption
        End Get
        Set
            _desciption = Value
            RaisePropertyChanged("VMDescription")
        End Set
    End Property

    Private _isBusy As Boolean
    Public Property VMisBusy As Boolean
        Get
            Return _isBusy
        End Get
        Set

            _isBusy = Value
            RaisePropertyChanged("VMisBusy")
        End Set
    End Property





#Region "INotifyPropertyChanged"

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


    ''' <summary>
    ''' Prozedur wirft den INotifyPropertyChanged Event welcher in der WPF benötigt wird um die UI zu verständingen
    ''' das eine Änderung an einem Property stattgefunden hat.
    ''' </summary>
    ''' <param name="prop">Das Propertie welche sich geändert hat. Ist Optional das der Parameter "CallerMemberName" verwendet</param>
    Protected Overridable Sub RaisePropertyChanged(ByVal prop As String)
        If prop IsNot Nothing AndAlso prop.Length > 0 Then RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
    End Sub


#End Region


#Region "INotifyPropertyChanging"
    Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging
    ''' <summary>
    ''' Prozedur wirft den INotifyPropertyChanging Event welcher in der WPF benötigt wird um die UI zu verständingen
    ''' das eine Änderung an einem PropertyChanged stattfinden wird.
    ''' </summary>
    ''' <param name="prop">Das Propertie welche sich geändert hat. Ist Optional das der Parameter "CallerMemberName" verwendet</param>
    Protected Overridable Sub RaisePropertyChanging(ByVal prop As String)
        If prop IsNot Nothing AndAlso prop.Length > 0 Then RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(prop))
    End Sub

#End Region

#Region "IDisposable Support"
    Private _disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then

            End If

        End If
        _disposedValue = True
    End Sub


    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.

        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


#End Region

End Class
