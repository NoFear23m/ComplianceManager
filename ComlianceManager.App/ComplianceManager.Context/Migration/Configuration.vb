
Imports System.Data.Entity.Migrations

Namespace Migrations

    Public NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of CompContext)

        Public Sub New()
            AutomaticMigrationsEnabled = True
            AutomaticMigrationDataLossAllowed = False

        End Sub


        Protected Overrides Sub Seed(context As CompContext)
            MyBase.Seed(context)

            UpdateDBEntrys(context)


        End Sub

        Private Sub UpdateDBEntrys(context As CompContext)
            'context.SettingCategories.AddOrUpdate(Function(c) c.Title,
            '                                      New SettingCategory() With {.Title = "Grundeinstellungen"},
            '                                      New SettingCategory() With {.Title = "Ordner/Pfade"},
            '                                      New SettingCategory() With {.Title = "Online"},
            '                                      New SettingCategory() With {.Title = "Format"},
            '                                      New SettingCategory() With {.Title = "Optik/Farben"},
            '                                      New SettingCategory() With {.Title = "Steuerelement-Wertespeicherung"},
            '                                      New SettingCategory() With {.Title = "Timer"},
            '                                      New SettingCategory() With {.Title = "Focuseinstellungen"},
            '                                      New SettingCategory() With {.Title = "Defaultwerte"})
            'context.Settings.AddOrUpdate(Function(c) c.Key,
            '                             New Model.Setting() With {.Key = "AttachmentsPath", .Title = "Der Pfad wo die Attachment gespeichert werden sollen.", .Value = "D:\Attachments\"},
            '                             New Model.Setting() With {.Key = "ParnterNetURL", .Title = "Die URL des Partner.Net Fensters mit den Reklamationen", .Value = "https://www.auto-partner.net/portal/at/thirdparty?directlink=KU_REK&noFramesMode=true"})
            'context.UserSettings.AddOrUpdate(Function(s) s.Title,
            '                                 New Model.UserSetting() With {.Title = "GridHidedColumns", .Value = "*;FallNr"})
        End Sub


    End Class
End Namespace