Public Class FormBarang

    Dim modeProses As Integer
    Dim baris As Integer

    Private Sub AturButton(st As Boolean)
        BtnTambah.Enabled = st
        BtnUbah.Enabled = st
        BtnHapus.Enabled = st
        BtnSimpan.Enabled = st
        BtnBatal.Enabled = st

        GroupBox1.Enabled = Not st
        GroupBox3.Enabled = st
        GroupBox4.Enabled = st
    End Sub

    Private Sub IsiBox(br As Integer)
        If br < DTGrid.Rows.Count Then
            With DGBarang.Rows(br)
                TxtKode.Text = .Cells(0).Value.ToString
                TxtNama.Text = .Cells(1).Value.ToString
                TxtHarga.Text = .Cells(2).Value.ToString
                TxtStok.Text = .Cells(3).Value.ToString
            End With
            LblBaris.Text = "Data ke-" & br + 1 & " dari " & DGBarang.RowCount - 1 & " data"
        End If
    End Sub

    Private Sub RefreshGrid()
        DTGrid = KontrolBarang.tampilData.ToTable
        DGBarang.DataSource = DTGrid

        If DTGrid.Rows.Count > 0 Then
            baris = DTGrid.Rows.Count - 1
            DGBarang.Rows(DTGrid.Rows.Count - 1).Selected = True
            DGBarang.CurrentCell = DGBarang.Item(1, baris)
            AturButton(True)
            IsiBox(baris)
        End If
    End Sub

    Private Sub TampilanCari()
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnTambah.Click
        TxtKode.Text = "B001"
        TxtNama.Text = "Rinso"
        DGBarang.DataSource = TxtKode

    End Sub

    Private Sub FormBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshGrid()
    End Sub


End Class
