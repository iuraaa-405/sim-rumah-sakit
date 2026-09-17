Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Threading
Imports DPUruNet
Imports DPUruNet.Constants
Imports System.Drawing.Imaging
Imports MySql.Data.MySqlClient

Public Class frmStaff
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As Integer
    Private isLoad As Boolean = False
    Private oStaff As New Reference.clsStaff
#End Region
#Region "Function"
    Private WithEvents enrollmentControl As DPCtlUruNet.EnrollmentControl
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As Integer = 0)
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True

        Try
            CurrentReader = ReaderCollection.GetReaders().FirstOrDefault()

            If enrollmentControl IsNot Nothing Then
                _enrollmentControl.Reader = CurrentReader
            Else
                enrollmentControl = New DPCtlUruNet.EnrollmentControl(CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)
                enrollmentControl.BackColor = System.Drawing.SystemColors.Window
                enrollmentControl.Dock = DockStyle.Left
                'enrollmentControl.Location = New System.Drawing.Point(20, 210)
                enrollmentControl.Name = "ctlEnrollmentControl"
                'enrollmentControl.Size = New System.Drawing.Size(482, 346)
                'enrollmentControl.TabIndex = 0
            End If

            Me.Controls.Add(enrollmentControl)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Staff.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose

            lKDSTAFF.Text = Staff.KDSTAFF
            lKDSTAFFBAGIAN.Text = Staff.KDSTAFFBAGIAN
            lKDSTAFFPANGKAT.Text = Staff.KDSTAFFPANGKAT
            lKDSTAFFJABATAN.Text = Staff.KDSTAFFJABATAN
            lKDSTAFFPENDIDIKAN.Text = Staff.KDSTAFFPENDIDIKAN
            lNAME_DISPLAY.Text = Staff.NAME_DISPLAY
            lNOMOR_NIP.Text = Staff.NOMOR_NIP
            lTMT.Text = Staff.TMT
            lTEMPATLAHIR.Text = Staff.TEMPATLAHIR
            lTANGGALLAHIR.Text = Staff.TANGGALLAHIR
            lJENISKELAMIN.Text = Staff.JENISKELAMIN
            lAGAMA.Text = Staff.AGAMA
            lNOMOR_HP1.Text = Staff.NOMOR_HP1
            lNOMOR_HP2.Text = Staff.NOMOR_HP2
            lEMAIL.Text = Staff.EMAIL
            lNOMOR_KTP.Text = Staff.NMOR_KTP
            lSTATUS.Text = Staff.STATUS
            lISACTIVE.Text = Staff.ISACTIVE
            lATTACMENT.Text = Staff.ATTACMENT
            lDESCRIPTION.Text = Staff.DESCRIPTION
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNAME_DISPLAY.Text.Trim.ToUpper

        Try
            enrollmentControl.Cancel()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDSTAFFBAGIAN()
        fn_LoadKDSTAFFPANGKAT()
        fn_LoadKDSTAFFJABATAN()
        fn_LoadKDSTAFFPENDIDIKAN()
        fn_LoadKDUSER()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnUpload.Enabled = Not Status

        grdKDSTAFFBAGIAN.Properties.ReadOnly = Status
        grdKDSTAFPANGKAT.Properties.ReadOnly = Status
        grdKDSTAFJABATAN.Properties.ReadOnly = Status
        grdKDSTAFFPENDIDIKAN.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        txtNOMOR_NIP.Properties.ReadOnly = Status
        deDATETMTKERJA.Properties.ReadOnly = Status
        txtTEMPATLAHIR.Properties.ReadOnly = Status
        deDATETANGGALLAHIR.Properties.ReadOnly = Status
        cboJENISKELAMIN.Properties.ReadOnly = Status
        cboAGAMA.Properties.ReadOnly = Status
        txtNOMOR_HP1.Properties.ReadOnly = Status
        txtNOMOR_HP2.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtNOMOR_KTP.Properties.ReadOnly = Status
        cboStatus.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDSTAFF.Text = "<--- AUTO --->"
        txtNAME_DISPLAY.ResetText()
        txtNOMOR_NIP.ResetText()
        deDATETMTKERJA.DateTime = Now
        txtTEMPATLAHIR.ResetText()
        deDATETANGGALLAHIR.DateTime = Now
        cboJENISKELAMIN.ResetText()
        cboAGAMA.SelectedIndex = 0
        txtNOMOR_HP1.ResetText()
        txtNOMOR_HP2.ResetText()
        txtEMAIL.ResetText()
        txtNOMOR_KTP.ResetText()
        cboStatus.ResetText()
        chkISACTIVE.Checked = True
        txtDESCRIPTION.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oStaff.GetData(sNoId)

            With ds
                grdKDUSER.Text = .NOIDUSER
                txtKDSTAFF.Text = sNoId
                grdKDSTAFFBAGIAN.Text = .KDSTAFFBAGIAN
                grdKDSTAFPANGKAT.Text = .KDSTAFFPANGKAT
                grdKDSTAFJABATAN.Text = .KDSTAFFJABATAN
                grdKDSTAFFPENDIDIKAN.Text = .KDSTAFFPENDIDIKAN
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                txtNOMOR_NIP.Text = .NOMOR_NIP
                deDATETMTKERJA.DateTime = .TMT
                txtTEMPATLAHIR.Text = .TEMPATLAHIR
                deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR
                cboJENISKELAMIN.SelectedIndex = .JENISKELAMIN
                cboAGAMA.SelectedIndex = .AGAMA
                txtNOMOR_HP1.Text = .NOMOR_HP1
                txtNOMOR_HP2.Text = .NOMOR_HP2
                txtEMAIL.Text = .EMAIL
                txtNOMOR_KTP.Text = .NOMOR_KTP
                cboStatus.Text = .STATUS
                chkISACTIVE.Checked = .ISACTIVE
                'IMAGE
                Try
                    Dim img = (From x In oStaff.GetData
                               Where x.KDSTAFF = sNoId
                               Select x.ATTACHMENT).Single

                    picGAMBAR.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
                Try
                    Fmds.Add(0, Importer.ImportFmd(ds.J0.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(1, Importer.ImportFmd(ds.J1.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(2, Importer.ImportFmd(ds.J2.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(3, Importer.ImportFmd(ds.J3.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(4, Importer.ImportFmd(ds.J4.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(5, Importer.ImportFmd(ds.J5.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(6, Importer.ImportFmd(ds.J6.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(7, Importer.ImportFmd(ds.J7.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(8, Importer.ImportFmd(ds.J8.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                Try
                    Fmds.Add(9, Importer.ImportFmd(ds.J9.ToArray(), Formats.Fmd.ANSI, Formats.Fmd.ANSI).Data)
                Catch ex As Exception

                End Try
                txtDESCRIPTION.Text = .DESCRIPTION
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()
        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
    End Function
    Private Function StreamToByteArray(ByVal inputStream As System.IO.MemoryStream) As Byte()
        Dim bytes = New Byte(16383) {}
        Using memoryStream = New System.IO.MemoryStream()
            Dim count As Integer
            While ((count = inputStream.Read(bytes, 0, bytes.Length)) > 0)

                memoryStream.Write(bytes, 0, count)

            End While

            Return memoryStream.ToArray

        End Using

    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNOMOR_KTP.Text = String.Empty Then
                txtNOMOR_KTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMOR_KTP.ErrorText = Statement.ErrorRequired

                txtNOMOR_KTP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDSTAFFBAGIAN.Text = String.Empty Then
                grdKDSTAFFBAGIAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSTAFFBAGIAN.ErrorText = Statement.ErrorRequired

                grdKDSTAFFBAGIAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDSTAFJABATAN.Text = String.Empty Then
                grdKDSTAFJABATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSTAFJABATAN.ErrorText = Statement.ErrorRequired

                grdKDSTAFJABATAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDSTAFPANGKAT.Text = String.Empty Then
                grdKDSTAFPANGKAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSTAFPANGKAT.ErrorText = Statement.ErrorRequired

                grdKDSTAFPANGKAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboJENISKELAMIN.Text = String.Empty Then
                cboJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboJENISKELAMIN.ErrorText = Statement.ErrorRequired

                cboJENISKELAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboAGAMA.Text = String.Empty Then
                cboAGAMA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboAGAMA.ErrorText = Statement.ErrorRequired

                cboAGAMA.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboStatus.Text = String.Empty Then
                cboStatus.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboStatus.ErrorText = Statement.ErrorRequired

                cboStatus.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim dsKTP = oStaff.GetDataByKTP(txtNOMOR_KTP.Text)
            If dsKTP IsNot Nothing Then
                If dsKTP.KDSTAFF <> sNoId Then
                    MsgBox("Nomor KTP Sudah digunakan oleh " & dsKTP.NAME_DISPLAY, MsgBoxStyle.Exclamation, Me.Text)
                    txtNOMOR_KTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOMOR_KTP.ErrorText = Statement.ErrorRequired

                    txtNOMOR_KTP.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oStaff.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oStaff.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSTAFF = sNoId
                .KDSTAFFBAGIAN = grdKDSTAFFBAGIAN.EditValue
                .KDSTAFFPANGKAT = grdKDSTAFPANGKAT.EditValue
                .KDSTAFFJABATAN = grdKDSTAFJABATAN.EditValue
                .KDSTAFFPENDIDIKAN = grdKDSTAFFPENDIDIKAN.EditValue
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.ToString.Trim
                .NOMOR_NIP = txtNOMOR_NIP.Text.ToString.Trim
                .TMT = deDATETMTKERJA.DateTime
                .TEMPATLAHIR = txtTEMPATLAHIR.Text.ToString.Trim
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
                .JENISKELAMIN = cboJENISKELAMIN.SelectedIndex
                .AGAMA = cboAGAMA.SelectedIndex
                .NOMOR_HP1 = txtNOMOR_HP1.Text.ToString.Trim.ToUpper
                .NOMOR_HP2 = txtNOMOR_HP2.Text.ToString.Trim.ToUpper
                .EMAIL = txtEMAIL.Text.ToString.Trim
                .NOMOR_KTP = txtNOMOR_KTP.Text.ToUpper.Trim.ToUpper
                .STATUS = cboStatus.Text
                .ISACTIVE = chkISACTIVE.Checked
                'IMAGE
                Try
                    Dim data As Byte() = System.IO.File.ReadAllBytes(picGAMBAR.ImageLocation)
                    .ATTACHMENT = data
                Catch oErr As Exception
                    If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                        Try
                            Dim img = (From x In oStaff.GetData
                                       Where x.KDSTAFF = sNoId
                                       Select x.ATTACHMENT).Single

                            .ATTACHMENT = img
                        Catch ex1 As Exception

                        End Try
                    End If
                End Try

                If Fmds.ContainsKey(0) = True Then
                    .J0 = Fmds.Item(0).Bytes
                End If
                If Fmds.ContainsKey(1) = True Then
                    .J1 = Fmds.Item(1).Bytes
                End If
                If Fmds.ContainsKey(2) = True Then
                    .J2 = Fmds.Item(2).Bytes
                End If
                If Fmds.ContainsKey(3) = True Then
                    .J3 = Fmds.Item(3).Bytes
                End If
                If Fmds.ContainsKey(4) = True Then
                    .J4 = Fmds.Item(4).Bytes
                End If
                If Fmds.ContainsKey(5) = True Then
                    .J5 = Fmds.Item(5).Bytes
                End If
                If Fmds.ContainsKey(6) = True Then
                    .J6 = Fmds.Item(6).Bytes
                End If
                If Fmds.ContainsKey(7) = True Then
                    .J7 = Fmds.Item(7).Bytes
                End If
                If Fmds.ContainsKey(8) = True Then
                    .J8 = Fmds.Item(8).Bytes
                End If
                If Fmds.ContainsKey(9) = True Then
                    .J9 = Fmds.Item(9).Bytes
                End If

                .NOIDUSER = grdKDUSER.EditValue
                .DESCRIPTION = txtDESCRIPTION.Text.Trim
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oStaff.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oStaff.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnUpload.Enabled = True Then
                    btnUpload_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"

            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnUpload_Click() Handles btnUpload.ItemClick
        If fileDialog.ShowDialog = DialogResult.OK Then
            picGAMBAR.Load(fileDialog.FileName)
            picGAMBAR.Update()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub SendMessage(ByVal message As String)
        txtMessage.Text += message & vbCr & vbLf & vbCr & vbLf
        txtMessage.SelectionStart = txtMessage.TextLength
        txtMessage.ScrollToCaret()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDSTAFFBAGIAN()
        Dim oStaffBagian As New Reference.clsStaffBagian
        Try
            grdKDSTAFFBAGIAN.Properties.DataSource = oStaffBagian.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTAFFBAGIAN.Properties.ValueMember = "KDSTAFFBAGIAN"
            grdKDSTAFFBAGIAN.Properties.DisplayMember = "MEMO"

            grdKDSTAFFBAGIAN.Text = oStaffBagian.GetData().Where(Function(x) x.ISDEFAULT = True).FirstOrDefault.KDSTAFFBAGIAN

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSTAFFPANGKAT()
        Dim oStaffPangkat As New Reference.clsStaffPangkat
        Try
            grdKDSTAFPANGKAT.Properties.DataSource = oStaffPangkat.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTAFPANGKAT.Properties.ValueMember = "KDSTAFFPANGKAT"
            grdKDSTAFPANGKAT.Properties.DisplayMember = "MEMO"

            grdKDSTAFPANGKAT.Text = oStaffPangkat.GetData().Where(Function(x) x.ISDEFAULT = True).FirstOrDefault.KDSTAFFPANGKAT

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSTAFFJABATAN()
        Dim oStaffJabtan As New Reference.clsStaffJabatan
        Try
            grdKDSTAFJABATAN.Properties.DataSource = oStaffJabtan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTAFJABATAN.Properties.ValueMember = "KDSTAFFJABATAN"
            grdKDSTAFJABATAN.Properties.DisplayMember = "MEMO"

            grdKDSTAFJABATAN.Text = oStaffJabtan.GetData().Where(Function(x) x.ISDEFAULT = True).FirstOrDefault.KDSTAFFJABATAN

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSTAFFPENDIDIKAN()
        Dim oStaffPendidikan As New Reference.clsStaffPendidikan
        Try
            grdKDSTAFFPENDIDIKAN.Properties.DataSource = oStaffPendidikan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSTAFFPENDIDIKAN.Properties.ValueMember = "KDSTAFFPENDIDIKAN"
            grdKDSTAFFPENDIDIKAN.Properties.DisplayMember = "MEMO"

            grdKDSTAFFPENDIDIKAN.Text = oStaffPendidikan.GetData().Where(Function(x) x.ISDEFAULT = True).FirstOrDefault.KDSTAFFPENDIDIKAN

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUSER()
        Dim oUSER As New Setting.clsUser
        Try
            grdKDUSER.Properties.DataSource = oUSER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUSER.Properties.ValueMember = "KDUSER"
            grdKDUSER.Properties.DisplayMember = "KDUSER"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Grid Method"

#End Region
#Region "Reader"
    Public Property Fmds() As Dictionary(Of Int16, Fmd)
        Get
            Return _fmds
        End Get
        Set(ByVal value As Dictionary(Of Int16, Fmd))
            _fmds = value
        End Set
    End Property
    Private _fmds As Dictionary(Of Int16, Fmd) = New Dictionary(Of Int16, Fmd)
    Public Property Reset() As Boolean
        Get
            Return _reset
        End Get
        Set(ByVal value As Boolean)
            _reset = value
        End Set
    End Property
    Private _reset As Boolean
    Public Property CurrentReader() As Reader
        Get
            Return _currentReader
        End Get
        Set(ByVal value As Reader)
            _currentReader = value
        End Set
    End Property
    Private _currentReader As Reader

    Public Function OpenReader() As Boolean
        _currentReader = ReaderCollection.GetReaders().FirstOrDefault()

        Reset = False
        Dim result As Constants.ResultCode = Constants.ResultCode.DP_DEVICE_FAILURE

        result = _currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE)

        If result <> Constants.ResultCode.DP_SUCCESS Then
            MessageBox.Show("Error:  " & result.ToString())
            Reset = True
            Return False
        End If

        Return True
    End Function
    Public Function StartCaptureAsync(ByVal OnCaptured As Reader.CaptureCallback) As Boolean
        AddHandler _currentReader.On_Captured, OnCaptured

        If Not CaptureFingerAsync() Then
            Return False
        End If

        Return True
    End Function
    Public Function CaptureFingerAsync() As Boolean
        Try
            GetStatus()

            Dim captureResult = _currentReader.CaptureAsync(Formats.Fid.ANSI,
                                                   CaptureProcessing.DP_IMG_PROC_DEFAULT,
                                                    _currentReader.Capabilities.Resolutions(0))

            If captureResult <> ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" + captureResult.ToString())
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show("Error:  " & ex.Message)
            Return False
        End Try
    End Function
    Public Sub GetStatus()
        Dim result = _currentReader.GetStatus()

        If (result <> ResultCode.DP_SUCCESS) Then
            If CurrentReader IsNot Nothing Then
                Reset = True
                Throw New Exception("" & result.ToString())
            End If
        End If

        If (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_BUSY) Then
            Thread.Sleep(50)
        ElseIf (_currentReader.Status.Status = ReaderStatuses.DP_STATUS_NEED_CALIBRATION) Then
            _currentReader.Calibrate()
        ElseIf (_currentReader.Status.Status <> ReaderStatuses.DP_STATUS_READY) Then
            Throw New Exception("Reader Status - " & CurrentReader.Status.Status.ToString())
        End If
    End Sub
    Public Function CheckCaptureResult(ByVal captureResult As CaptureResult) As Boolean
        If captureResult.Data Is Nothing Then
            If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
                Reset = True
                Throw New Exception("" & captureResult.ResultCode.ToString())
            End If

            If captureResult.Quality <> Constants.CaptureQuality.DP_QUALITY_CANCELED Then
                Throw New Exception("Quality - " & captureResult.Quality.ToString())
            End If
            Return False
        End If
        Return True
    End Function
    Public Function CreateBitmap(ByVal bytes As [Byte](), ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim rgbBytes As Byte() = New Byte(bytes.Length * 3 - 1) {}

        For i As Integer = 0 To bytes.Length - 1
            rgbBytes((i * 3)) = bytes(i)
            rgbBytes((i * 3) + 1) = bytes(i)
            rgbBytes((i * 3) + 2) = bytes(i)
        Next
        Dim bmp As New Bitmap(width, height, PixelFormat.Format24bppRgb)

        Dim data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.[WriteOnly], PixelFormat.Format24bppRgb)

        For i As Integer = 0 To bmp.Height - 1
            Dim p As New IntPtr(data.Scan0.ToInt64() + data.Stride * i)
            System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3)
        Next

        bmp.UnlockBits(data)

        Return bmp
    End Function
    Public Sub CancelCaptureAndCloseReader(ByVal OnCaptured As Reader.CaptureCallback)
        If _currentReader IsNot Nothing Then
            ' Dispose of reader handle and unhook reader events.
            CurrentReader.Dispose()

            If (Reset) Then
                CurrentReader = Nothing
            End If
        End If
    End Sub
#End Region
#Region "Enrollment Control Events"
    Private Sub enrollment_OnCancel(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnCancel
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnCancel:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnCancel:  No Reader Connected, finger " & fingerPosition)
        End If
    End Sub

    Private Sub enrollment_OnCaptured(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal captureResult As CaptureResult, ByVal fingerPosition As Integer) Handles enrollmentControl.OnCaptured
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage(("OnCaptured:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition & ", quality ") + captureResult.Quality.ToString())
        Else
            SendMessage("OnCaptured:  No Reader Connected, finger " & fingerPosition)
        End If

        If captureResult.ResultCode <> Constants.ResultCode.DP_SUCCESS Then
            If CurrentReader IsNot Nothing Then
                CurrentReader.Dispose()
                CurrentReader = Nothing
            End If

            ' Disconnect reader from enrollment control
            _enrollmentControl.Reader = Nothing
            MessageBox.Show("Error:  " & captureResult.ResultCode.ToString())
        Else
            If captureResult.Data IsNot Nothing Then
                For Each fiv As Fid.Fiv In captureResult.Data.Views
                    picFinger.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height)
                Next
            End If
        End If
    End Sub
    Private Sub enrollment_OnDelete(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnDelete
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnDelete:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnDelete:  No Reader Connected, finger " & fingerPosition)
        End If

        Fmds.Remove(fingerPosition)
    End Sub
    Private Sub enrollment_OnEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As DataResult(Of Fmd), ByVal fingerPosition As Integer) Handles enrollmentControl.OnEnroll
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnEnroll:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnEnroll:  No Reader Connected, finger " & fingerPosition)
        End If

        ' Save the enrollment to file.
        If result IsNot Nothing AndAlso result.Data IsNot Nothing Then
            If Fmds.ContainsKey(fingerPosition) = True Then
                Fmds.Remove(fingerPosition)
            End If

            Fmds.Add(fingerPosition, result.Data)
        End If
    End Sub
    Private Sub enrollment_OnStartEnroll(ByVal enrollmentControl As DPCtlUruNet.EnrollmentControl, ByVal result As Constants.ResultCode, ByVal fingerPosition As Integer) Handles enrollmentControl.OnStartEnroll
        If enrollmentControl.Reader IsNot Nothing Then
            SendMessage("OnStartEnroll:  " & Convert.ToString(enrollmentControl.Reader.Description.Name) & ", finger " & fingerPosition)
        Else
            SendMessage("OnStartEnroll:  No Reader Connected, finger " & fingerPosition)
        End If
    End Sub
#End Region

End Class