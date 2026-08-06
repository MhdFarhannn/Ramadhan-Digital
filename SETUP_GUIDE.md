# 🔧 Setup & Quick Start Guide

## ✅ Prerequisites

Pastikan Anda sudah melakukan:

```bash
# 1. Install ClosedXML package (sesuai instruksi Anda)
dotnet add package ClosedXML

# 2. Restore dependencies
dotnet restore

# 3. Build project
dotnet build
```

---

## 🚀 Quick Start

### 1. Login sebagai Admin

```bash
curl -X POST "https://localhost:5001/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"username":"admin_username","password":"admin_password"}'
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin_username",
  "nama": "Administrator",
  "role": "Admin",
  "kelas": "",
  "refreshToken": "..."
}
```

### 2. Siapkan File Excel

Buat file Excel dengan struktur yang **BERBEDA** sesuai kebutuhan:

#### Untuk Import Siswa (IdRole = 3)
Buat file `siswa.xlsx`:
```
Nama              | Username      | Password
==============================================
Ahmad             | ahmad123      | qwerty123
Siti              | siti456       | password123
Aisyah            | aisyah789     | aisyah@2024
```

**Kolom:** Hanya Nama, Username, Password (3 kolom)

#### Untuk Import Guru (IdRole = 2)
Buat file `guru.xlsx`:
```
Nama              | Username      | Password
==============================================
Dr. Abdullah      | dr.abdullah   | guru@2024
Ibu Nurul         | ibu.nurul     | guru@2024
```

**Kolom:** Hanya Nama, Username, Password (3 kolom)

### 3. Upload File

#### Untuk Siswa (dengan parameter idKelas):
```bash
# Menambahkan siswa ke kelas dengan ID = 1
curl -X POST "https://localhost:5001/api/v1/auth/register-bulk-excel-siswa?idKelas=1" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -F "file=@siswa.xlsx"
```

#### Untuk Guru (tanpa parameter IdKelas):
```bash
curl -X POST "https://localhost:5001/api/v1/auth/register-bulk-excel-guru" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -F "file=@guru.xlsx"
```

### 4. Lihat Response

Jika sukses:
```json
{
  "success": true,
  "importedCount": 3,
  "errors": [],
  "message": "Berhasil mengimpor 3 siswa ke kelas 1"
}
```

Jika ada error:
```json
{
  "success": false,
  "importedCount": 1,
  "errors": [
	"Baris 3: Username 'ahmad123' sudah terdaftar"
  ],
  "message": "Gagal mengimpor data siswa dari Excel"
}
```

---

## 📝 Membuat Sample Excel File (Manual)

Jika ingin membuat sample file secara manual:

### Menggunakan Microsoft Excel

1. Buka Excel
2. Row 1 (Header): `Nama` | `Username` | `Password` | `IdRole` | `IdKelas`
3. Row 2+: Input data user
4. Save as: `sample_users.xlsx`

### Menggunakan LibreOffice Calc

1. Buka Calc
2. Buat struktur header yang sama
3. Input data
4. File → Save As → Format: Excel 2007-365 (.xlsx)

### Menggunakan Google Sheets

1. Buat spreadsheet baru
2. Input data dengan header
3. File → Download → Excel (.xlsx)

---

## 🧪 Testing Scenarios

### Scenario 1: Import Sukses Penuh
**File:** 3 users baru dengan username unik

**Expected Response:**
```json
{
  "success": true,
  "importedCount": 3,
  "errors": [],
  "message": "Berhasil mengimpor 3 user"
}
```

**Verifikasi:** Check database, pastikan 3 user ada

### Scenario 2: Import Partial (Ada Duplikat)
**File:** 3 users, username #1 sudah ada

**Expected Response:**
```json
{
  "success": false,
  "importedCount": 2,
  "errors": ["Baris 2: Username 'ahmad123' sudah terdaftar"],
  "message": "Gagal mengimpor data dari Excel"
}
```

**Verifikasi:** 2 user ditambah, 1 dilewatkan

### Scenario 3: Validasi Field Kosong
**File:** Ada baris dengan Nama kosong

**Expected Response:**
```json
{
  "success": false,
  "importedCount": 0,
  "errors": ["Baris 3: Kolom 'Nama' tidak boleh kosong"],
  "message": "Gagal mengimpor data dari Excel"
}
```

### Scenario 4: Format File Salah
**File:** Mengirim file .csv atau .txt

**Expected Response:**
```json
{
  "success": false,
  "importedCount": 0,
  "errors": [],
  "message": "Format file harus .xlsx atau .xls"
}
```

### Scenario 5: Tanpa Authorization
**Request:** POST tanpa header Authorization

**Expected Response:**
```json
(401 Unauthorized)
```

---

## 🛠️ Manual Testing Checklist

- [ ] File Excel valid (.xlsx format)
- [ ] Header row sesuai format
- [ ] No duplicate username dalam file
- [ ] IdRole valid (1, 2, atau 3)
- [ ] Password tidak kosong
- [ ] Nama tidak kosong
- [ ] Login sebagai Admin
- [ ] Token valid (tidak expired)
- [ ] Upload file ke endpoint yang benar
- [ ] Cek response status dan pesan

---

## 📊 Monitoring Import

### Cek User yang Baru Diimport

```bash
# Login lalu cek database
SELECT * FROM users WHERE created_at > NOW() - INTERVAL '1 hour';
```

### Cek Log Import

Setiap import tercatat dalam response dengan:
- Jumlah sukses
- List error per baris
- User yang berhasil added

---

## 🔒 Security Reminders

⚠️ **Important:**
1. Ubah password default di Excel SEBELUM import ke production
2. Jangan share Excel file yang berisi password
3. Pastikan file dihapus setelah import
4. Password otomatis di-hash, jangan share raw password
5. Hanya Admin yang bisa import

---

## 🐛 Debugging

### Jika error "Operator '?' cannot be applied"
- Ini sudah di-fix dalam implementation
- Jika masih terjadi, rebuild project: `dotnet build`

### Jika error "File tidak ditemukan"
- Pastikan file ada dan path benar
- Gunakan full path jika perlu

### Jika error "Token expired/invalid"
- Login kembali untuk mendapatkan token baru
- Copy token baru ke Authorization header

### Jika import gagal tapi tidak ada error message
- Check error list di response
- Lihat server logs untuk detail lebih
- Pastikan database connection aktif

---

## 📈 Performance Tips

Untuk file besar (>1000 baris):

1. **Split file** menjadi beberapa bagian
2. **Increase timeout** di client jika ada
3. **Check database** resourcenya cukup
4. **Monitor server logs** saat processing berlangsung

---

## 🎓 Learning Resources

- ClosedXML Documentation: https://github.com/ClosedXML/ClosedXML
- ASP.NET Core API: https://docs.microsoft.com/aspnet
- Dapper ORM: https://github.com/DapperLib/Dapper

---

**Ready to import bulk users! 🎉**
