-- =============================================================================
-- 000_create_app_user.sql
-- Tạo user Oracle cho ứng dụng (chạy bằng SYS hoặc admin)
-- Điều chỉnh password trước khi chạy production
-- =============================================================================

-- Oracle 26ai Free trên máy này: port 1522, PDB FREEPDB1
-- Chạy toàn bộ setup (user + bảng):
--   sqlplus "/ as sysdba" @run_setup.sql
--
-- Hoặc từng bước thủ công:

CREATE USER APP_USER IDENTIFIED BY "YOUR_PASSWORD"
  DEFAULT TABLESPACE USERS
  TEMPORARY TABLESPACE TEMP
  QUOTA UNLIMITED ON USERS;

GRANT CONNECT, RESOURCE TO APP_USER;
GRANT CREATE SESSION TO APP_USER;
GRANT CREATE TABLE TO APP_USER;
GRANT CREATE SEQUENCE TO APP_USER;
GRANT CREATE VIEW TO APP_USER;

-- Sau đó connect APP_USER và chạy 001_create_tables.sql