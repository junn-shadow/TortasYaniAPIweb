# Backup-Database.ps1
# -------------------------------------------------
# Copia el archivo tortasyani.db a la carpeta backups/
# Añadiendo la fecha y hora al nombre del archivo.
# Manteniendo sólo los últimos 30 backups.
# -------------------------------------------------

$dbPath = "e:\TortasYaniAPI\tortasyani.db"
$backupDir = "e:\TortasYaniAPI\backups"

# Crear la carpeta backups si no existe
if (-not (Test-Path $backupDir)) {
    New-Item -ItemType Directory -Path $backupDir | Out-Null
}

if (-not (Test-Path $dbPath)) {
    Write-Warning "El archivo de base de datos no existe en: $dbPath"
    Exit
}

# Obtener fecha actual formateada
$timestamp = Get-Date -Format "yyyy-MM-dd_HH-mm-ss"
$backupFile = Join-Path $backupDir "tortasyani_$timestamp.db"

# Intentar realizar la copia de seguridad
try {
    Copy-Item -Path $dbPath -Destination $backupFile -Force
    Write-Host "✅ Copia de seguridad creada con éxito en: $backupFile"
    
    # Mantener solo los últimos 30 backups
    $maxBackups = 30
    $backups = Get-ChildItem -Path $backupDir -Filter "tortasyani_*.db" | Sort-Object CreationTime -Descending
    if ($backups.Count -gt $maxBackups) {
        $backupsToDelete = $backups[$maxBackups..($backups.Count - 1)]
        foreach ($file in $backupsToDelete) {
            Remove-Item -Path $file.FullName -Force
            Write-Host "🗑️ Copia antigua eliminada: $($file.Name)"
        }
    }
}
catch {
    Write-Error "❌ Error al crear la copia de seguridad: $_"
}
