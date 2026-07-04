-- =============================================================================
-- 001_pkg_task_move.sql
-- Oracle package MOVE_TASK — Phase 8 (optional, chạy với APP_USER)
-- App mặc định dùng EF transaction; script này để học SP + có thể gọi từ C# sau.
-- =============================================================================

CREATE OR REPLACE PACKAGE PKG_TASK AS
    PROCEDURE MOVE_TASK (
        P_TASK_ID        IN  NUMBER,
        P_USER_ID        IN  NUMBER,
        P_PROJECT_ID     IN  NUMBER,
        P_NEW_STATUS     IN  NUMBER,
        P_NEW_SORT_ORDER IN  NUMBER,
        P_RESULT         OUT NUMBER,
        P_MESSAGE        OUT VARCHAR2
    );
END PKG_TASK;
/

CREATE OR REPLACE PACKAGE BODY PKG_TASK AS

    PROCEDURE MOVE_TASK (
        P_TASK_ID        IN  NUMBER,
        P_USER_ID        IN  NUMBER,
        P_PROJECT_ID     IN  NUMBER,
        P_NEW_STATUS     IN  NUMBER,
        P_NEW_SORT_ORDER IN  NUMBER,
        P_RESULT         OUT NUMBER,
        P_MESSAGE        OUT VARCHAR2
    ) IS
        V_OWNER   NUMBER(1);
        V_OLD_STATUS NUMBER;
    BEGIN
        P_RESULT := 0;
        P_MESSAGE := NULL;

        -- Kiểm tra project thuộc user
        SELECT COUNT(*)
          INTO V_OWNER
          FROM PROJECTS P
         WHERE P.PROJECT_ID = P_PROJECT_ID
           AND P.USER_ID = P_USER_ID
           AND P.IS_DELETED = 0;

        IF V_OWNER = 0 THEN
            P_MESSAGE := 'Project not found or access denied';
            RETURN;
        END IF;

        SELECT T.STATUS
          INTO V_OLD_STATUS
          FROM TASKS T
         WHERE T.TASK_ID = P_TASK_ID
           AND T.PROJECT_ID = P_PROJECT_ID
           AND T.IS_DELETED = 0
           FOR UPDATE;

        IF P_NEW_STATUS NOT BETWEEN 1 AND 3 THEN
            P_MESSAGE := 'Invalid status';
            RETURN;
        END IF;

        -- Cập nhật task được kéo
        UPDATE TASKS
           SET STATUS = P_NEW_STATUS,
               SORT_ORDER = P_NEW_SORT_ORDER
         WHERE TASK_ID = P_TASK_ID
           AND PROJECT_ID = P_PROJECT_ID
           AND IS_DELETED = 0;

        IF SQL%ROWCOUNT = 0 THEN
            P_MESSAGE := 'Task not found';
            ROLLBACK;
            RETURN;
        END IF;

        COMMIT;
        P_RESULT := 1;
        P_MESSAGE := 'OK';
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            ROLLBACK;
            P_MESSAGE := 'Task not found';
        WHEN OTHERS THEN
            ROLLBACK;
            P_MESSAGE := SUBSTR(SQLERRM, 1, 400);
    END MOVE_TASK;

END PKG_TASK;
/
