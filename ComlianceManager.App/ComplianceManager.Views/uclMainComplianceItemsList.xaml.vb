Imports System.ComponentModel

Public Class uclMainComplianceItemsList
    Implements INotifyPropertyChanged



    Private _HiddenGridColumns As List(Of String)
    Public Property HiddenGridColumns() As List(Of String)
        Get
            Return _HiddenGridColumns
        End Get
        Set(ByVal value As List(Of String))
            _HiddenGridColumns = value
            RaisePropertie("HiddenGridColumns")
        End Set
    End Property

    Private Sub CheckComboHiddenCols_ItemSelectionChanged(sender As Object, e As Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs)
        If e.IsSelected = False Then
            Me.HiddenGridColumns.Add(DirectCast(e.Item, DataGridColumn).Header)
            HideGridColumns()
        Else
            Me.HiddenGridColumns.Remove(DirectCast(e.Item, DataGridColumn).Header)
            HideGridColumns()
        End If
        Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
        vm.HidedColumnsString = Me.HiddenGridColumns
    End Sub

    Private Sub HideGridColumns()

        For Each c As DataGridColumn In Me.CompliantGrid.Columns
            If c.Header IsNot Nothing AndAlso HiddenGridColumns.FirstOrDefault(Function(n) n.ToLower = c.Header.ToString.ToLower) IsNot Nothing Then
                c.Visibility = Windows.Visibility.Hidden
            Else
                c.Visibility = Windows.Visibility.Visible
            End If
        Next

    End Sub

    Private Sub uclMainComplianceItemsList_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If ComponentModel.DesignerProperties.GetIsInDesignMode(Me) Then Exit Sub
        Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext

        Me.HiddenGridColumns = vm.HidedColumnsString
        'Me.HiddenGridColumns = Split(SPS.VKM.DBHelper.UserSettings.GetCurrentUserSetting("DisabledCarListColumns", Instance.CurrentLoggedinUser.ID).ToString, ";").ToList
        ''Me.AviableGridColumns = Me.ActsGrid.Columns

        For i As Integer = 0 To Me.CompliantGrid.Columns.Count - 1
            Dim HidColText As String = Me.CompliantGrid.Columns(i).Header.ToString.ToLower
            If Me.HiddenGridColumns.FirstOrDefault(Function(n) n.ToLower = HidColText) Is Nothing Then Me.CheckComboHiddenCols.SelectedItems.Add(Me.CheckComboHiddenCols.Items(i))
        Next


        HideGridColumns()
    End Sub

    Public Sub RaisePropertie(p1 As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(p1))
    End Sub

    Private Sub uclMainComplianceItemsList_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

    End Sub

    Private Sub CompliantGrid_Sorting(sender As Object, e As DataGridSortingEventArgs) Handles CompliantGrid.Sorting
        'Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
        'If vm IsNot Nothing Then

        '    'vm.SaveSortingString()
        'End If

    End Sub

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

End Class
