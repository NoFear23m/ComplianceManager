Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure

Public Class ComplianteItemsVM
    Inherits ViewModelBase

    Friend _mainVM As MainVM
    Friend _context As Context.CompContext
    Private showOnProps As Boolean = False

    Public Sub New()
        If ComponentModel.DesignerProperties.GetIsInDesignMode(New Windows.DependencyObject) Then Exit Sub
        If _context Is Nothing Then _context = New Context.CompContext

        Load()

        showOnProps = True
    End Sub

    Public Sub New(mainVM As MainVM)
        _mainVM = mainVM
        If _context Is Nothing Then _context = New Context.CompContext
        ComplianceItems = New ObservableCollection(Of ComplianceItemVM)
        ComplianceItemsView = Windows.Data.CollectionViewSource.GetDefaultView(ComplianceItems)

        Try
            Dim currUSer As Model.User = _context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            ColumnOrder = currUSer.UserSettings.Where(Function(s) s.Title = "GridColumnsOrder").FirstOrDefault.Value.Split(";").ToList
        Catch ex As Exception
            MsgBox("Fehler beim holden von ColumnOrder")
        End Try

        Try
            Dim currUSer As Model.User = _context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            ColumnWidths = currUSer.UserSettings.Where(Function(s) s.Title = "GridColumnsWidth").FirstOrDefault.Value.Split(";").ToList
        Catch ex As Exception
            MsgBox("Fehler beim holden von ColumnOrder")
        End Try

        Load()

        Try
            Dim currUSer As Model.User = _context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            HidedColumnsString = New List(Of String)
            If currUSer Is Nothing Then
                MsgBox("Kein User gefunden!!!")
            Else
                Dim sett As Model.UserSetting = currUSer.UserSettings.Where(Function(s) s.Title = "GridHidedColumns").FirstOrDefault

                If sett Is Nothing Then MsgBox("sett was Nothing!!")
                HidedColumnsString = Split(sett.Value, ";").ToList
            End If
        Catch ex As Exception
            MsgBox("Fehler beim HidedColumnsString-LoadedMainVM")
        End Try


        showOnProps = True
    End Sub





    Private _hidedColumnsString As List(Of String)
    Public Property HidedColumnsString() As List(Of String)
        Get
            Return _hidedColumnsString
        End Get
        Set(ByVal value As List(Of String))
            _hidedColumnsString = value
            If showOnProps = False Then Exit Property
            Using db As New Context.CompContext
                Dim currUSer As Model.User = db.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).First
                currUSer.UserSettings.Where(Function(s) s.Title = "GridHidedColumns").FirstOrDefault.Value = String.Join(";", _hidedColumnsString)
                db.SaveChanges()
            End Using
        End Set
    End Property







    Private Function GetSortDescriptionToString() As String
        Dim ret As String = ""
        For Each item In ComplianceItemsView.SortDescriptions
            ret += String.Format("{0},{1};", CInt(item.Direction), item.PropertyName)
        Next
        Return ret
    End Function


    Private Sub GetSortdescriptionFromString(desc As String)
        If String.IsNullOrWhiteSpace(desc) Then Exit Sub

        Dim FirstLevel As List(Of String) = desc.Split(";", options:=StringSplitOptions.RemoveEmptyEntries).ToList

        ComplianceItemsView.SortDescriptions.Clear()

        For Each item In FirstLevel
            Dim SecondLevel As List(Of String) = item.Split(",", options:=StringSplitOptions.RemoveEmptyEntries).ToList
            ComplianceItemsView.SortDescriptions.Add(New SortDescription(SecondLevel(1), CInt(SecondLevel(0))))
        Next
        ComplianceItemsView.Refresh()

    End Sub

    Public Sub SaveSortingString()
        Using db As New Context.CompContext
            Dim currUSer As Model.User = db.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            Dim sett = currUSer.UserSettings.Where(Function(s) s.Title = "GridSorting").FirstOrDefault
            sett.Value = GetSortDescriptionToString()
            Dim res As Integer = db.SaveChanges()
            Debug.WriteLine(res.ToString)
        End Using
    End Sub







    Private _ColumnsOrder As List(Of String)
    Public Property ColumnOrder() As List(Of String)
        Get
            Return _ColumnsOrder
        End Get
        Set(ByVal value As List(Of String))
            _ColumnsOrder = value
        End Set
    End Property




    Private Function ConvertOrderListToString(order As List(Of String)) As String
        Return String.Join(";", order)
    End Function

    Public Sub SaveOrderList()
        Using db As New Context.CompContext
            Dim currUSer As Model.User = db.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            Dim sett = currUSer.UserSettings.Where(Function(s) s.Title = "GridColumnsOrder").FirstOrDefault
            sett.Value = ConvertOrderListToString(ColumnOrder)
            Dim res As Integer = db.SaveChanges()
            Debug.WriteLine(res.ToString)
        End Using
    End Sub



    Private _columnWidths As List(Of String)
    Public Property ColumnWidths() As List(Of String)
        Get
            Return _columnWidths
        End Get
        Set(ByVal value As List(Of String))
            _columnWidths = value
        End Set
    End Property

    Private Function ConvertWidthsListToString(widths As List(Of String)) As String
        Return String.Join(";", widths)
    End Function


    Public Sub SaveWidthsList()
        Using db As New Context.CompContext
            Dim currUSer As Model.User = db.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
            Dim sett = currUSer.UserSettings.Where(Function(s) s.Title = "GridColumnsWidth").FirstOrDefault
            sett.Value = ConvertWidthsListToString(ColumnWidths)
            Dim res As Integer = db.SaveChanges()
            Debug.WriteLine(res.ToString)
        End Using
    End Sub


    Friend Sub Load()
        '     If ComplianceItemsView IsNot Nothing Then Debug.WriteLine(String.Join(",", ComplianceItemsView.SortDescriptions))
        _context = New Context.CompContext
        Dim itemsQuery = _context.ComplianceItems.OrderByDescending(Function(o) o.LastChange)
        If Not IncludeDeleted Then itemsQuery = itemsQuery.Where(Function(d) d.IsDeleted = False)
        If ShowOnlyOpenItems Then itemsQuery = itemsQuery.Where(Function(i) i.FinishedAt Is Nothing)

        Dim items As List(Of Model.CompliantItem) = itemsQuery.ToList

        ComplianceItems.Clear()

        For Each i As Model.CompliantItem In items
            ComplianceItems.Add(New ComplianceItemVM(i, _context, Me))
        Next

        Dim currUSer As Model.User = _context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
        GetSortdescriptionFromString(currUSer.UserSettings.Where(Function(s) s.Title = "GridSorting").FirstOrDefault.Value)

        ComplianceItemsView.Refresh()
    End Sub


    Private _showOnlyOpenItems As Boolean = True
    Public Property ShowOnlyOpenItems() As Boolean
        Get
            Return _showOnlyOpenItems
        End Get
        Set(ByVal value As Boolean)
            _showOnlyOpenItems = value
            RaisePropertyChanged("ShowOnlyOpenItems")
            If showOnProps Then Load()
        End Set
    End Property

    Private _includeDeleted As Boolean = False
    Public Property IncludeDeleted() As Boolean
        Get
            Return _includeDeleted
        End Get
        Set(ByVal value As Boolean)
            _includeDeleted = value
            RaisePropertyChanged("IncludeDeleted")
            If showOnProps Then Load()
        End Set
    End Property


    Private _complianceItems As ObservableCollection(Of ComplianceItemVM)

    Public Property ComplianceItems() As ObservableCollection(Of ComplianceItemVM)
        Get
            Return _complianceItems
        End Get
        Set(ByVal value As ObservableCollection(Of ComplianceItemVM))
            _complianceItems = value
            RaisePropertyChanged("ComplianceItems")
        End Set
    End Property

    Private _complianceItemsView As ICollectionView
    Public Property ComplianceItemsView() As ICollectionView
        Get
            Return _complianceItemsView
        End Get
        Set(ByVal value As ICollectionView)
            _complianceItemsView = value
            RaisePropertyChanged("ComplianceItemsView")
        End Set
    End Property

    Private _filterString As String
    Public Property FilterString() As String
        Get
            Return _filterString
        End Get
        Set(ByVal value As String)
            _filterString = value
            ComplianceItemsView.Filter = New Predicate(Of Object)(AddressOf Filter_Queue)
            RaisePropertyChanged("FilterString")

        End Set
    End Property






    Private Function Filter_Queue(obj As Object) As Boolean
        Dim item As ComplianceItemVM = obj
        If FilterString Is Nothing OrElse String.IsNullOrEmpty(FilterString) Then Return True
        Return item.FullName.ToLower.Contains(FilterString.ToLower) OrElse
                item.CreatedByUserName.ToLower.Contains(FilterString.ToLower) OrElse
               (item.LicensePlate IsNot Nothing AndAlso item.LicensePlate.ToLower.Contains(FilterString.ToLower))

    End Function



    Private _reload As ICommand
    Public Property ReloadCommand() As Windows.Input.ICommand
        Get
            If _reload Is Nothing Then _reload = New RelayCommand(AddressOf ReloadCommand_Execute)
            Return _reload
        End Get
        Set(ByVal value As ICommand)
            _reload = value
            RaisePropertyChanged("Reload")
        End Set
    End Property

    Private Sub ReloadCommand_Execute(obj As Object)
        _mainVM.RefreshViews()
    End Sub
End Class
