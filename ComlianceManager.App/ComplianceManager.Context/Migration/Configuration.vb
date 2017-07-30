
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

        End Sub


    End Class
End Namespace