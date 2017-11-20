<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.openReports = New System.Windows.Forms.Button()
        Me.openAmazon = New System.Windows.Forms.Button()
        Me.openLog = New System.Windows.Forms.Button()
        Me.openDoneReports = New System.Windows.Forms.Button()
        Me.openLogFolder = New System.Windows.Forms.Button()
        Me.openDebug = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'openReports
        '
        Me.openReports.Location = New System.Drawing.Point(12, 12)
        Me.openReports.Name = "openReports"
        Me.openReports.Size = New System.Drawing.Size(100, 23)
        Me.openReports.TabIndex = 0
        Me.openReports.Text = "Reports"
        Me.openReports.UseVisualStyleBackColor = True
        '
        'openAmazon
        '
        Me.openAmazon.Location = New System.Drawing.Point(12, 70)
        Me.openAmazon.Name = "openAmazon"
        Me.openAmazon.Size = New System.Drawing.Size(100, 23)
        Me.openAmazon.TabIndex = 1
        Me.openAmazon.Text = "Amazon"
        Me.openAmazon.UseVisualStyleBackColor = True
        '
        'openLog
        '
        Me.openLog.Location = New System.Drawing.Point(118, 41)
        Me.openLog.Name = "openLog"
        Me.openLog.Size = New System.Drawing.Size(100, 23)
        Me.openLog.TabIndex = 2
        Me.openLog.Text = "Log"
        Me.openLog.UseVisualStyleBackColor = True
        '
        'openDoneReports
        '
        Me.openDoneReports.Location = New System.Drawing.Point(118, 12)
        Me.openDoneReports.Name = "openDoneReports"
        Me.openDoneReports.Size = New System.Drawing.Size(100, 23)
        Me.openDoneReports.TabIndex = 3
        Me.openDoneReports.Text = "Done Reports"
        Me.openDoneReports.UseVisualStyleBackColor = True
        '
        'openLogFolder
        '
        Me.openLogFolder.Location = New System.Drawing.Point(12, 41)
        Me.openLogFolder.Name = "openLogFolder"
        Me.openLogFolder.Size = New System.Drawing.Size(100, 23)
        Me.openLogFolder.TabIndex = 4
        Me.openLogFolder.Text = "Log Folder"
        Me.openLogFolder.UseVisualStyleBackColor = True
        '
        'openDebug
        '
        Me.openDebug.Location = New System.Drawing.Point(118, 70)
        Me.openDebug.Name = "openDebug"
        Me.openDebug.Size = New System.Drawing.Size(100, 23)
        Me.openDebug.TabIndex = 5
        Me.openDebug.Text = "Debug"
        Me.openDebug.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(225, 107)
        Me.Controls.Add(Me.openDebug)
        Me.Controls.Add(Me.openLogFolder)
        Me.Controls.Add(Me.openDoneReports)
        Me.Controls.Add(Me.openLog)
        Me.Controls.Add(Me.openAmazon)
        Me.Controls.Add(Me.openReports)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "Form1"
        Me.Text = "AmazonTranslator 2.1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents openReports As Button
    Friend WithEvents openAmazon As Button
    Friend WithEvents openLog As Button
    Friend WithEvents openDoneReports As Button
    Friend WithEvents openLogFolder As Button
    Friend WithEvents openDebug As Button
End Class
