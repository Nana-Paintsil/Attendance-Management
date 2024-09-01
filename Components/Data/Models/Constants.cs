
namespace AttendanceManagement.Handlers
{
    public static class AppConstants
    {
      public static string initialQuery = @"-- TABLE
CREATE TABLE [AcademicYears] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[Year] text  NOT NULL COLLATE NOCASE
	,[IsActive] integer  NOT NULL 
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id]));
CREATE TABLE [AcademicYears_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [AttendanceRecords] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[EmployeeId] text  NOT NULL COLLATE NOCASE
	,[SignInTime] text  NULL 
	,[SignOutTime] text  NULL 
	,[Date] text  NOT NULL 
	,PRIMARY KEY ([Id])
	FOREIGN KEY ( [EmployeeId]) REFERENCES [Employees]( [Id] )
);
CREATE TABLE [AttendanceRecords_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [Employees] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[Username] text  NOT NULL COLLATE NOCASE
	,[Email] text  NOT NULL COLLATE NOCASE
	,[Title] text  NOT NULL COLLATE NOCASE
	,[FirstName] text  NOT NULL COLLATE NOCASE
	,[LastName] text  NOT NULL COLLATE NOCASE
	,[Telephone] text  NOT NULL COLLATE NOCASE
	,[Role] text  NOT NULL COLLATE NOCASE
	,[TypeOfStaffId] text  NOT NULL COLLATE NOCASE
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id])
	FOREIGN KEY ( [TypeOfStaffId]) REFERENCES [TypeOfStaffs]( [Id] )
);
CREATE TABLE [Employees_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [Holidays] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[StartDate] text  NOT NULL 
	,[EndDate] text  NOT NULL 
	,[Name] text  NOT NULL COLLATE NOCASE
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id]));
CREATE TABLE [Holidays_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [Institutions] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[CPD] text  NOT NULL COLLATE NOCASE
	,[Code] text  NOT NULL COLLATE NOCASE
	,[District] text  NOT NULL COLLATE NOCASE
	,[Location] text  NOT NULL COLLATE NOCASE
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id]));
CREATE TABLE [Institutions_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [Leaves] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[EmployeeID] text  NOT NULL COLLATE NOCASE
	,[StartDate] text  NOT NULL 
	,[EndDate] text  NOT NULL 
	,[Reason] text  NOT NULL COLLATE NOCASE
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id])
	FOREIGN KEY ( [EmployeeID]) REFERENCES [Employees]( [Id] )
);
CREATE TABLE [Leaves_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [LogEntries] (
	[Id] integer   
	,[Initiator] text  NOT NULL COLLATE NOCASE
	,[Action] text  NOT NULL COLLATE NOCASE
	,[TableName] text  NULL COLLATE NOCASE
	,[DateTime] text  NOT NULL 
	,[OldValues] text  NOT NULL COLLATE NOCASE
	,[NewValues] text  NOT NULL COLLATE NOCASE
	,[AffectedColumns] text  NOT NULL COLLATE NOCASE
	,PRIMARY KEY ([Id]));
CREATE TABLE [LogEntries_tracking] (
[Id] integer NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [scope_info](
                        sync_scope_name text NOT NULL,
                        sync_scope_schema text NULL,
                        sync_scope_setup text NULL,
                        sync_scope_version text NULL,
                        sync_scope_last_clean_timestamp integer NULL,
                        sync_scope_properties text NULL,
                        CONSTRAINT PKey_scope_info PRIMARY KEY(sync_scope_name));
CREATE TABLE [scope_info_client](
                        sync_scope_id blob NOT NULL,
                        sync_scope_name text NOT NULL,
                        sync_scope_hash text NOT NULL,
                        sync_scope_parameters text NULL,
                        scope_last_sync_timestamp integer NULL,
                        scope_last_server_sync_timestamp integer NULL,
                        scope_last_sync_duration integer NULL,
                        scope_last_sync datetime NULL,
                        sync_scope_errors text NULL,
                        sync_scope_properties text NULL,
                        CONSTRAINT PKey_scope_info_client PRIMARY KEY(sync_scope_id, sync_scope_name, sync_scope_hash));
CREATE TABLE [Tenants] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[ParentTenantId] text  NOT NULL COLLATE NOCASE
	,[ShortName] text  NOT NULL COLLATE NOCASE
	,[FullName] text  NOT NULL COLLATE NOCASE
	,[HierarchyLevel] integer  NOT NULL 
	,PRIMARY KEY ([Id]));
CREATE TABLE [Tenants_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [TenantUsers] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[Email] text  NOT NULL COLLATE NOCASE
	,PRIMARY KEY ([Id])
	FOREIGN KEY ( [TenantId]) REFERENCES [Tenants]( [Id] )
);
CREATE TABLE [TenantUsers_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [Terms] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[Name] text  NOT NULL COLLATE NOCASE
	,[StartDate] text  NOT NULL 
	,[EndDate] text  NOT NULL 
	,[AcademicYearId] text  NOT NULL COLLATE NOCASE
	,[IsActive] integer  NOT NULL 
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id])
	FOREIGN KEY ( [AcademicYearId]) REFERENCES [AcademicYears]( [Id] )
);
CREATE TABLE [Terms_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [TypeOfStaffs] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[StaffType] text  NOT NULL COLLATE NOCASE
	,[IdPrefix] text  NOT NULL COLLATE NOCASE
	,[IsDeleted] integer  NOT NULL 
	,PRIMARY KEY ([Id]));
CREATE TABLE [TypeOfStaffs_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
CREATE TABLE [UserAuths] (
	[Id] text  NOT NULL COLLATE NOCASE
	,[TenantId] text  NOT NULL COLLATE NOCASE
	,[Email] text  NOT NULL COLLATE NOCASE
	,[Username] text  NOT NULL COLLATE NOCASE
	,[PasswordHash] text  NOT NULL COLLATE NOCASE
	,PRIMARY KEY ([Id]));
CREATE TABLE [UserAuths_tracking] (
[Id] text NOT NULL COLLATE NOCASE, 
[update_scope_id] [text] NULL COLLATE NOCASE, 
[timestamp] [integer] NULL, 
[sync_row_is_tombstone] [integer] NOT NULL default(0), 
[last_change_datetime] [datetime] NULL, 
 PRIMARY KEY ([Id]));
 
-- INDEX
CREATE INDEX [AcademicYears_tracking_timestamp_index] ON [AcademicYears_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [AttendanceRecords_tracking_timestamp_index] ON [AttendanceRecords_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Employees_tracking_timestamp_index] ON [Employees_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Holidays_tracking_timestamp_index] ON [Holidays_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Institutions_tracking_timestamp_index] ON [Institutions_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Leaves_tracking_timestamp_index] ON [Leaves_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [LogEntries_tracking_timestamp_index] ON [LogEntries_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [TenantUsers_tracking_timestamp_index] ON [TenantUsers_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Tenants_tracking_timestamp_index] ON [Tenants_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [Terms_tracking_timestamp_index] ON [Terms_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [TypeOfStaffs_tracking_timestamp_index] ON [TypeOfStaffs_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
CREATE INDEX [UserAuths_tracking_timestamp_index] ON [UserAuths_tracking] (
	 [timestamp] ASC
	,[update_scope_id] ASC
	,[sync_row_is_tombstone] ASC
	,[Id] ASC
);
 
-- TRIGGER
CREATE TRIGGER [AcademicYears_delete_trigger] AFTER DELETE ON [AcademicYears] 

BEGIN
	INSERT OR REPLACE INTO [AcademicYears_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [AcademicYears_insert_trigger] AFTER INSERT ON [AcademicYears] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [AcademicYears_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [AcademicYears_update_trigger] AFTER UPDATE ON [AcademicYears] 

Begin 
	UPDATE [AcademicYears_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [AcademicYears_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [AcademicYears_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [AcademicYears_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [AttendanceRecords_delete_trigger] AFTER DELETE ON [AttendanceRecords] 

BEGIN
	INSERT OR REPLACE INTO [AttendanceRecords_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [AttendanceRecords_insert_trigger] AFTER INSERT ON [AttendanceRecords] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [AttendanceRecords_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [AttendanceRecords_update_trigger] AFTER UPDATE ON [AttendanceRecords] 

Begin 
	UPDATE [AttendanceRecords_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [AttendanceRecords_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [AttendanceRecords_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [AttendanceRecords_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Employees_delete_trigger] AFTER DELETE ON [Employees] 

BEGIN
	INSERT OR REPLACE INTO [Employees_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Employees_insert_trigger] AFTER INSERT ON [Employees] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Employees_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Employees_update_trigger] AFTER UPDATE ON [Employees] 

Begin 
	UPDATE [Employees_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Employees_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Employees_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Employees_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Holidays_delete_trigger] AFTER DELETE ON [Holidays] 

BEGIN
	INSERT OR REPLACE INTO [Holidays_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Holidays_insert_trigger] AFTER INSERT ON [Holidays] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Holidays_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Holidays_update_trigger] AFTER UPDATE ON [Holidays] 

Begin 
	UPDATE [Holidays_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Holidays_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Holidays_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Holidays_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Institutions_delete_trigger] AFTER DELETE ON [Institutions] 

BEGIN
	INSERT OR REPLACE INTO [Institutions_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Institutions_insert_trigger] AFTER INSERT ON [Institutions] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Institutions_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Institutions_update_trigger] AFTER UPDATE ON [Institutions] 

Begin 
	UPDATE [Institutions_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Institutions_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Institutions_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Institutions_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Leaves_delete_trigger] AFTER DELETE ON [Leaves] 

BEGIN
	INSERT OR REPLACE INTO [Leaves_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Leaves_insert_trigger] AFTER INSERT ON [Leaves] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Leaves_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Leaves_update_trigger] AFTER UPDATE ON [Leaves] 

Begin 
	UPDATE [Leaves_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Leaves_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Leaves_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Leaves_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [LogEntries_delete_trigger] AFTER DELETE ON [LogEntries] 

BEGIN
	INSERT OR REPLACE INTO [LogEntries_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [LogEntries_insert_trigger] AFTER INSERT ON [LogEntries] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [LogEntries_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [LogEntries_update_trigger] AFTER UPDATE ON [LogEntries] 

Begin 
	UPDATE [LogEntries_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [LogEntries_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [LogEntries_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [LogEntries_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [TenantUsers_delete_trigger] AFTER DELETE ON [TenantUsers] 

BEGIN
	INSERT OR REPLACE INTO [TenantUsers_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [TenantUsers_insert_trigger] AFTER INSERT ON [TenantUsers] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [TenantUsers_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [TenantUsers_update_trigger] AFTER UPDATE ON [TenantUsers] 

Begin 
	UPDATE [TenantUsers_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [TenantUsers_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [TenantUsers_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [TenantUsers_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Tenants_delete_trigger] AFTER DELETE ON [Tenants] 

BEGIN
	INSERT OR REPLACE INTO [Tenants_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Tenants_insert_trigger] AFTER INSERT ON [Tenants] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Tenants_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Tenants_update_trigger] AFTER UPDATE ON [Tenants] 

Begin 
	UPDATE [Tenants_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Tenants_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Tenants_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Tenants_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [Terms_delete_trigger] AFTER DELETE ON [Terms] 

BEGIN
	INSERT OR REPLACE INTO [Terms_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [Terms_insert_trigger] AFTER INSERT ON [Terms] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [Terms_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [Terms_update_trigger] AFTER UPDATE ON [Terms] 

Begin 
	UPDATE [Terms_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [Terms_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [Terms_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [Terms_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [TypeOfStaffs_delete_trigger] AFTER DELETE ON [TypeOfStaffs] 

BEGIN
	INSERT OR REPLACE INTO [TypeOfStaffs_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [TypeOfStaffs_insert_trigger] AFTER INSERT ON [TypeOfStaffs] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [TypeOfStaffs_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [TypeOfStaffs_update_trigger] AFTER UPDATE ON [TypeOfStaffs] 

Begin 
	UPDATE [TypeOfStaffs_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [TypeOfStaffs_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [TypeOfStaffs_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [TypeOfStaffs_tracking] WHERE [Id]=new.[Id])=0
; 
End;
CREATE TRIGGER [UserAuths_delete_trigger] AFTER DELETE ON [UserAuths] 

BEGIN
	INSERT OR REPLACE INTO [UserAuths_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		old.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,1
		,datetime('now')
	);
END;
CREATE TRIGGER [UserAuths_insert_trigger] AFTER INSERT ON [UserAuths] 

BEGIN
-- If row was deleted before, it already exists, so just make an update
	INSERT OR REPLACE INTO [UserAuths_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	VALUES (
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	);
END;
CREATE TRIGGER [UserAuths_update_trigger] AFTER UPDATE ON [UserAuths] 

Begin 
	UPDATE [UserAuths_tracking] 
	SET [update_scope_id] = NULL -- scope id is always NULL when update is made locally
		,[timestamp] = replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,[last_change_datetime] = datetime('now')
	Where [UserAuths_tracking].[Id] = new.[Id]; 
	INSERT OR IGNORE INTO [UserAuths_tracking] (
		[Id]
		,[update_scope_id]
		,[timestamp]
		,[sync_row_is_tombstone]
		,[last_change_datetime]
	) 
	SELECT 
		new.[Id]
		,NULL
		,replace(strftime('%Y%m%d%H%M%f', 'now'), '.', '')
		,0
		,datetime('now')
	WHERE (SELECT COUNT(*) FROM [UserAuths_tracking] WHERE [Id]=new.[Id])=0
; 
End;
 
-- VIEW
 
INSERT INTO UserAuths VALUES ('B3683420-1F3A-4236-9BA6-2C7B82B97581','00000000-0000-0000-0000-000000000000','nanapaintsil27@gmail.com','BMA000','9A852549FB8907227F5DEE28618F7BDF3DA87F4517A8DDE7513BC81C4CD48D60:38D9E8B7CB8351275BBDA45C359FE36F:50000:SHA256');";
    }
}
