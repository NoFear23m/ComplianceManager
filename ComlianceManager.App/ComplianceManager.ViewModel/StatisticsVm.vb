Imports System.Windows.Media
Imports LiveCharts
Imports LiveCharts.Defaults
Imports SPS.ViewModel.Infrastructure


Public Class StatisticsVm
    Inherits ViewModelBase

    Public Sub New()
        StatStartDate = DateAdd(DateInterval.Month, -6, Now)
        StatEndDate = Now


        CompliantCountPerMonthStats = New SeriesCollection
        MonthValues = New List(Of String)
        If ComponentModel.DesignerProperties.GetIsInDesignMode(New Windows.DependencyObject) Then
            MonthValues = New List(Of String) From {"Jänner", "Fabruar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"}
            CompliantCountPerMonthStats.Add(New Wpf.LineSeries() With {.Title = "Anzahl des Reklamationen", .Values = New ChartValues(Of Integer) From {10, 4, 20, 20, 10, 14}})
            CompliantCountPerMonthStats.Add(New Wpf.LineSeries() With {.Title = "Abschluss in Tagen", .Values = New ChartValues(Of Integer) From {2, 1, 18, 32, 1, 4}})
        Else
            CalculateStatistics()
        End If

    End Sub


    Private _statStartDate As Date
    Public Property StatStartDate() As Date
        Get
            Return _statStartDate
        End Get
        Set(ByVal value As Date)
            _statStartDate = value
            RaisePropertyChanged("StatStartDate")
            CalculateStatistics()
        End Set
    End Property


    Private _statEnddate As Date
    Public Property StatEndDate() As Date
        Get
            Return _statEnddate
        End Get
        Set(ByVal value As Date)
            _statEnddate = value
            RaisePropertyChanged("StatEndDate")
            CalculateStatistics()
        End Set
    End Property



    Private Sub CalculateStatistics()

        CompliantCountPerMonthStats = New SeriesCollection
        ReasonCountstats = New SeriesCollection
        EntryTypeStats = New SeriesCollection

        MonthValues = New List(Of String)

        Using db As New Context.CompContext
            _aviableEntryTypes = (From et In db.EntryTypes Where et.IsDeleted = False Select et.EntryTitle).ToList
            _aviableReasons = (From re In db.Resons Where re.IsDeleted = False Select re.ReasonTitle).ToList
            RaisePropertyChanged(NameOf(AviableentryTypes)) : RaisePropertyChanged(NameOf(AviableReasons))

            'Das erste Liniendiagram
            Dim Allcompliants = db.ComplianceItems.Where(Function(c) c.IsDeleted = False AndAlso c.FinishedAt IsNot Nothing AndAlso c.CreationDate > StatStartDate AndAlso c.FinishedAt.Value < StatEndDate).ToList

            Dim montChartValues As New ChartValues(Of Integer)
            Dim montLineSerie = New Wpf.LineSeries() With {.Title = "Anz. Reklamationen", .Values = New ChartValues(Of Integer)}
            Dim editDaysLineSerie = New Wpf.LineSeries() With {.Title = "Bearbeitungsdauer Tage", .Values = New ChartValues(Of Integer)}

            For i As Integer = 0 To 11
                MonthValues.Add(MonthName(StatStartDate.AddMonths(i).Month))
                montLineSerie.Values.Add(Allcompliants.Where(Function(c) c.CreationDate.Month = StatStartDate.AddMonths(i).Month).Count)
                editDaysLineSerie.Values.Add(Allcompliants.Where(Function(c) c.CreationDate.Month = StatStartDate.AddMonths(i).Month).Sum(Function(s) s.EditTimeInDays))
            Next
            CompliantCountPerMonthStats.Add(montLineSerie)
            CompliantCountPerMonthStats.Add(editDaysLineSerie)

            'Dim AbteilungenSerie = New Wpf.PieSeries() With {.Title = "Anz. nach Abteilungen", .Values = New ChartValues(Of Integer)}
            ReasonCountstats.Add(New Wpf.RowSeries() With {.Title = "Reklamationsgrund", .Values = New ChartValues(Of Double), .Fill = New SolidColorBrush(Colors.Green)})


            For Each r In AviableReasons
                Dim c = Allcompliants.Where(Function(c1) c1.ComplianceReason.ReasonTitle = r).Count
                'ReasonCountstats.Add(New Wpf.RowSeries() With {.Title = r.ReasonTitle, .Values = New ChartValues(Of Integer) From {c}, .DataLabels = True, .ToolTip = String.Format("{0}: {1} Reklamationen", r.ReasonTitle, c)})
                ReasonCountstats(0).Values.Add(CDbl(c))
            Next

            EntryTypeStats.Add(New Wpf.RowSeries() With {.Title = "Reklamationsherkunft", .Values = New ChartValues(Of Double)})
            For Each r In AviableentryTypes
                Dim c = Allcompliants.Where(Function(c1) c1.ComplianceEntryType.EntryTitle = r).Count
                'EntryTypeStats.Add(New Wpf.RowSeries() With {.Title = r.EntryTitle, .Values = New ChartValues(Of Integer) From {c}, .DataLabels = True, .ToolTip = String.Format("{0}: {1} Reklamationen", r.EntryTitle, c)})
                EntryTypeStats(0).Values.Add(CDbl(c))
            Next



        End Using

    End Sub



    Private _compliantCountPerMonthStats As SeriesCollection
    Public Property CompliantCountPerMonthStats() As SeriesCollection
        Get
            Return _compliantCountPerMonthStats
        End Get
        Set(ByVal value As SeriesCollection)
            _compliantCountPerMonthStats = value
            RaisePropertyChanged("CompliantCountPerMonthStats")
        End Set
    End Property


    Private _monthValues As List(Of String)
    Public Property MonthValues() As List(Of String)
        Get
            Return _monthValues
        End Get
        Set(ByVal value As List(Of String))
            _monthValues = value
            RaisePropertyChanged("MonthValues")
        End Set
    End Property


    Private _reasonCountStats As SeriesCollection
    Public Property ReasonCountstats() As SeriesCollection
        Get
            Return _reasonCountStats
        End Get
        Set(ByVal value As SeriesCollection)
            _reasonCountStats = value
            RaisePropertyChanged("ReasonCountstats")
        End Set
    End Property

    Private _entryTypeStats As SeriesCollection
    Public Property EntryTypeStats() As SeriesCollection
        Get
            Return _entryTypeStats
        End Get
        Set(ByVal value As SeriesCollection)
            _entryTypeStats = value
            RaisePropertyChanged("entryTypeStats")
        End Set
    End Property


    Private _aviableReasons As List(Of String)
    Public ReadOnly Property AviableReasons As List(Of String)
        Get
            Return _aviableReasons
        End Get
    End Property

    Public Property ReasonsColors As New LiveCharts.Wpf.ColorsCollection From {Colors.Red, Colors.Blue, Colors.YellowGreen, Colors.Green, Colors.Brown, Colors.DeepPink}

    Private _aviableEntryTypes As List(Of String)
    Public ReadOnly Property AviableentryTypes As List(Of String)
        Get
            Return _aviableEntryTypes
        End Get
    End Property


    Public ReadOnly Property ReasonFormatter(val As Double) As String
        Get
            Return val.ToString("N")
        End Get
    End Property

End Class
