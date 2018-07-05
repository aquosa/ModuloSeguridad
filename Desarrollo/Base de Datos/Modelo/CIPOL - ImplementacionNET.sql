-- PARA ORACLE: 
--  Remover SET NOCOUNT ON, SET NOCOUNT OFF, reemplazar GETDATE() por SYSDATE.
--  Utilizar los inserts de SE_HISTORIAL_USUARIO para oracle, ver mas abajo.

SET NOCOUNT ON
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_AUTORIZACION
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_AUTORIZACION (IDAUTORIZACION,CODAUTORIZ,DESCAUTORIZ)VALUES(0,'S/A','Tareas Primitivas sin autorizaci�n');
INSERT INTO SE_AUTORIZACION (IDAUTORIZACION,CODAUTORIZ,DESCAUTORIZ)VALUES(1,'1A','Tareas Primitivas que requieren 1 Autorizante');
INSERT INTO SE_AUTORIZACION (IDAUTORIZACION,CODAUTORIZ,DESCAUTORIZ)VALUES(2,'2/A','Tareas Primitivas que requieren 2 Autorizantes');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_MENUES
-------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------
-- Tabla SIST_KDOCUMENTOS
-------------------------------------------------------------------------------------------------------------
INSERT INTO SIST_KDOCUMENTOS (IDTIPODOC,TIPODOCUMENTO,TIPOABREVIADO,DOCBAJA)VALUES(0,'NO ESPECIFICADO','N/A','N');
-------------------------------------------------------------------------------------------------------------
-- Tabla SIST_KAREAS
-------------------------------------------------------------------------------------------------------------
INSERT INTO SIST_KAREAS (IDAREA,NOMBREAREA,RESPONSABLE,CARGORESPONSABLE,COMENTARIOS,BAJA,FICTICIA)VALUES(0,' NO ESPECIFICADA','','','  NO ESPECIFICADA','N','N');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_GRUPO_TAREA
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_GRUPO_TAREA (IDGRUPO,DESCGRUPO)VALUES(0,'GRUPO RAIZ');
INSERT INTO SE_GRUPO_TAREA (IDGRUPO,DESCGRUPO)VALUES(1,'SIR - Seguridad');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_CODAUDITORIA
-------------------------------------------------------------------------------------------------------------
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(100,'Intento de uso de la terminal @ ,no habilitada para el uso de la aplicaci�n');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(110,'El usuario inexistente @, efectu� un intento de inicio de sesi�n en la terminal @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(120,'El usuario @ posee un bloqueo generado por exceso de intentos de inicio de sesi�n con contrase�a inv�lida');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(130,'El usuario @ posee un bloqueo generado por procesos de seguridad');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(140,'El usuario @ intent� iniciar una sesi�n con una contrase�a inv�lida');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(150,'El usuario @ provoc� el bloqueo de su cuenta debido al exceso de intentos de inicio de sesi�n con contrase�a inv�lida');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(160,'El usuario @ respondi� afirmativamente a la propuesta anticipada de cambio de contrase�a por vencimiento cercano');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(170,'El usuario @ comenz� el proceso de cambio de contrase�a debido al vencimiento de la misma');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(180,'El usuario @ especific� una contrase�a anterior inv�lida cuando efectuaba el proceso de cambio de la misma');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(190,'El usuario @ especific� una dupla de contrase�as que no son coincidentes, cuando efectuaba el proceso de cambio de la misma');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(200,'La longitud de la nueva contrase�a especificada por el usuario @ no cumple con la longitud m�nima requerida');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(205,'El nivel de complejidad de la nueva contrase�a especificada por el usuario @ no cumple con el  establecido en la pol�tica');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(210,'Se rechaz� la nueva contrase�a propuesta por el usuario @ debido a la existencia de la misma en el archivo hist�rico');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(220,'Se rechaz� el intento de inicio de sesi�n del usuario @ debido a que su contrase�a ha caducado');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(230,'Se rechaz� el intento de inicio de sesi�n del usuario @ debido a que no est� autorizado para el uso de la terminal @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(240,'Se rechaz� el intento de inicio de sesi�n del usuario @ debido a la restricci�n horaria que posee');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(250,'Se rechaz� el intento de inicio de sesi�n del usuario @ en la terminal @ debido a que ya posee otra sesi�n activa');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(260,'En la terminal @ se especific� una contrase�a err�nea para el usuario autorizante @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(270,'En la terminal @ se ha excedido la cantidad de intentos fallidos al especificar la contrase�a  el usuario autorizante @. Se cancel� la tarea involucrada.');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(400,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la cantidad de tiempo para bloqueo de terminal por inactividad. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(410,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la cantidad de intentos de inicio de sesi�n inv�lidos admitidos. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(420,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, modific� la cantidad de tiempo a partir de la cual se desbloquean las cuentas de usuario. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(430,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la longitud m�nima de contrase�as. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(440,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la duraci�n m�xima de las contrase�as. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(450,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la duraci�n m�nima de las contrase�as. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(460,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� el cambio de la cantidad de d�as de antelaci�n para aviso de pr�ximo vencimiento de la contrase�a de usuario. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(470,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en la cantidad de contrase�as que se almacenan para control de repetici�n. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(480,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en la cantidad de d�as admitidos sin inicio de sesi�n. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(490,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en la cantidad de minutos de pr�rroga para el horario de los usuarios. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(500,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en la cantidad de minutos de antelaci�n para el aviso de finalizaci�n del horario admitido. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(510,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en la cantidad de minutos para bloquear el panel de ejecuci�n por inactividad. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(520,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en el modo de asignaci�n de tareas y roles. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(530,'El usuario @ en el contexto de administrador de la seguridad desde la PC @, efectu� la modificaci�n en el nivel de complejidad exigido para la contrase�a. Valor existente: @, nuevo valor: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(600,'El usuario @ en el contexto de administrador de la seguridad, efectu� el desbloqueo de la cuenta del usuario @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(610,'El usuario @ en el contexto de administrador de la seguridad, efectu� el bloqueo de la cuenta del usuario @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(620,'El usuario @ en el contexto de administrador de la seguridad, efectu� el cambio de la contrase�a del usuario @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(700,'El usuario @ en el contexto de administrador de la seguridad, efectu� un ABM de grupos de exclusi�n');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(710,'El usuario @ en el contexto de administrador de la seguridad, efectu� un ABM de roles');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(720,'El usuario @ en el contexto de administrador de la seguridad, efectu� un ABM de tareas de autorizaci�n');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(730,'El usuario @ en el contexto de administrador de la seguridad, efectu� modificaciones en las pertenencias de tareas a los grupos de exclusi�n');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(740,'El usuario @ en el contexto de administrador de la seguridad, efectu� modificaciones en las pertenencias de tareas a los roles');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(750,'El usuario @ en el contexto de administrador de la seguridad, efectu� el alta del usuario: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(760,'El usuario @ en el contexto de administrador de la seguridad, efectu� la baja del usuario: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(770,'El usuario @ en el contexto de administrador de la seguridad, efectu� cambios en los roles asociados al usuario: @');
INSERT INTO [se_codauditoria] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(780,'El usuario @ en el contexto de administrador de la seguridad, efectu� la activaci�n del usuario: @');
-- 25-04-2014 EmilianoD: GCPCambiosWeb: 15293 Agregar registro de LOGIN/LOGOUT al aplicativo
INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(0,'El usuario @ realizo el login desde la terminal @');
INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(790,'El usuario @ realizo el cierre de sesi�n desde (@ - @).');
INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(1,'El usuario @ realizo el logout desde la terminal @');
INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(2,'Se cerr� la sesi�n en la terminal @ por superar el l�mite de tiempo permitido');
INSERT INTO [SE_CODAUDITORIA] ([CODAUDITORIA],[TEXTOAUDITORIA])VALUES(3,'El usuario @ cerr� involuntariamente la sesi�n en la terminal @');

-------------------------------------------------------------------------------------------------------------
-- Tabla SE_MENSAJES
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(0,'Proceso exitoso.');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(10,'Terminal bloqueada. Consulte al administrador del sistema');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(15,'Terminal no habilitada. Consulte al administrador del sistema');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(17,'Imposible iniciar una sesi�n para el usuario especificado');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(18,'El usuario tiene una sesi�n iniciada en la terminal @. Imposible continuar');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(20,'No se puede iniciar la sesi�n. Verifique la cuenta y la contrase�a y reintente, si no es posible nuevamente consulte al administrador');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(25,'Falta 1 d�a para que caduque su contrase�a. �Desea efectuar ahora la renovaci�n?');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(30,'Faltan @ d�as para que caduque su contrase�a. �Desea efectuar ahora la renovaci�n?');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(35,'La contrase�a ha caducado, a continuaci�n comenzar� el proceso de renovaci�n');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(40,'Usuario no autorizado para el uso de esta terminal. Consulte al administrador del sistema');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(45,'Horario de trabajo no autorizado para el usuario especificado. Consulte al administrador del sistema');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(50,'La contrase�a especificada no cumple los requisitos establecidos');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(55,'No hay coincidencia entre la nuevas contrase�as  indicadas. Ingr�selas nuevamente');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(60,'El administrador ha forzado el cambio de contrase�a. A continuaci�n comenzar� el proceso');
INSERT INTO SE_MENSAJES (CODMENSAJE,TEXTOMENSAJE)VALUES(100,'Contrase�a incorrecta para el usuario autorizante especificado. Verifique y reintente.');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_Parametros
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_Parametros (COLUMNA1,COLUMNA2,COLUMNA3,COLUMNA4,COLUMNA5)VALUES(NULL,NULL,NULL,'XVAJy3tM4Wlf9UyqyLRY3hVL1FYQPNc3aHslt/pIsgu0u8ws4yIm1IsCRhLs0XdqGnlL/8KYxJEc7WZOXlSKBNRdFKttPlya','dcEyNHR0CRI=');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_USUARIOS
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_USUARIOS (IDUSUARIO,USUARIO,NOMBRES,DOMICILIO,FECHAULTUSOCTA,NRODOCUMENTO,CANTINTINVUSOCTA,FORZARCAMBIO,CTABLOQUEADA,COMENTARIO,FECHABLOQUEO,FECHAALTA,FECHABAJA,IDAREA,IDTIPODOC,ULTIMOSISTEMA,ALIAS_USUARIO,IDUSRALTA,IDUSRBAJA,EMAIL)
VALUES(0,'master','SuperUsuario','',GETDATE(),'','WuMtVHk8o#U=','iHLrZpbUtT8=','iHLrZpbUtT8=','',NULL,GETDATE(),NULL,0,0,NULL,'',0,NULL,NULL);
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_HORARIOS_USUARIO
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(1,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(2,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(3,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(4,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(5,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(6,0,23);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,0);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,1);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,2);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,3);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,4);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,5);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,6);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,7);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,8);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,9);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,10);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,11);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,12);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,13);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,14);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,15);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,16);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,17);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,18);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,19);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,20);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,21);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,22);
INSERT INTO SE_HORARIOS_USUARIO (IDDIA,IDUSUARIO,IDHORARIO)VALUES(7,0,23);
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_SIST_HABILITADOS
-------------------------------------------------------------------------------------------------------------
--[LeandroF]	21/09/2015 - TFS-WorkItem: 4974
INSERT INTO SE_SIST_HABILITADOS (IDSISTEMA,CODSISTEMA,DESCSISTEMA,FECHAHABILITACION,NOMBREEXEC,SISTEMAHABILITADO,ICONO,OBSERVACIONES,PAGINAPORDEFECTO,DESCRIPCIONCORTA,IMPACTACAJA)VALUES(1,'Seguridad','Sistema de Seguridad',GETDATE(),NULL,'S','icon-locked*blue','uvFHCab2NYhgd4#xNGFYJCxloC8kwlUY','','','');
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_HISTORIAL_USUARIO
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,0,GETDATE(),GETDATE(),'dvNTtGmZsDmup6P2YP/Dv2Au7eM=');
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,1,GETDATE(),GETDATE(),'OJBcKiPkBqQM7qlJohGrx6Ns7VM=');
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,2,GETDATE(),GETDATE(),'OucZFRq60eDKN005n2UoJQXwuDE=');

/* PARA ORACLE
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,0,to_date('12/03/2008 1:41:41','MM/DD/YYYY HH24:MI:SS'),TO_DATE('02/01/2009 1:41:41','MM/DD/YYYY HH24:MI:SS'),'dvNTtGmZsDmup6P2YP/Dv2Au7eM=');
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,1,to_date('12/03/2008 1:41:26','MM/DD/YYYY HH24:MI:SS'),TO_DATE('02/01/2009 1:41:26','MM/DD/YYYY HH24:MI:SS'),'OJBcKiPkBqQM7qlJohGrx6Ns7VM=');
INSERT INTO SE_HISTORIAL_USUARIO (IDUSUARIO,ORDEN,FECHAULTACT,FECHAVENCIMIENTO,SINONIMO)VALUES(0,2,to_date('12/03/2008 1:41:13','MM/DD/YYYY HH24:MI:SS'),TO_DATE('02/01/2009 1:41:13','MM/DD/YYYY HH24:MI:SS'),'OucZFRq60eDKN005n2UoJQXwuDE=');
*/

-------------------------------------------------------------------------------------------------------------
-- Tabla SE_TAREAS
-------------------------------------------------------------------------------------------------------------
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1001,'SE1','Administraci�n de Usuarios','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1002,'SE2','Administraci�n de Roles','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1003,'SE3','Habilitaci�n de Terminales operativas','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1004,'SE4','Agrupamiento de Tareas','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1005,'SE5','Definici�n de Pol�ticas generales','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1006,'SE6','Tareas Autorizantes','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1007,'SE7','Monitor de Actividades','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1009,'SE9','Ver Reportes','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1010,'SE10','Ver Registros de Auditor�a','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1011,'SE11','Administraci�n de Areas','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1012,'SE12','Administraci�n de Sistemas Bloqueados','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1013,'SE13','Activar Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1014,'SE14','Administraci�n de Sistemas','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1015,'SE15','Administraci�n de Tareas Primitivas','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1016,'SE16','An�lisis de Auditor�a','MA4phPHBUyA=',0,1,0);
INSERT INTO SE_TAREAS (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1017,'SE17','Cambiar Dominio y Organizaci�n','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1020,'SE20','Administraci�n de Usuarios - Ocultar Bot�n Nuevo Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1021,'SE21','Administraci�n de Usuarios - Ocultar Bot�n Modificar Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1022,'SE22','Administraci�n de Usuarios - Ocultar Bot�n Eliminar Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1023,'SE23','Administraci�n de Usuarios - Ocultar Bot�n Terminales Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1024,'SE24','Administraci�n de Usuarios - Ocultar Bot�n Horarios Usuario','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1025,'SE25','Administraci�n de Usuarios - Ocultar Bot�n Agregar Rol','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1026,'SE26','Administraci�n de Usuarios - Ocultar Bot�n Quitar Rol','MA4phPHBUyA=',0,1,0);
INSERT INTO se_tareas (IDTAREA,CODIGOTAREA,DESCRIPCIONTAREA,REQUIEREAUDITORIA,IDAUTORIZACION,IDSISTEMA,IDGRUPO)VALUES(1027,'SE27','Administraci�n de Usuarios - No Modificar Usuarios con tareas de CIPOL','MA4phPHBUyA=',0,1,0);
-------------------------------------------------------------------------------------------------------------
-- Tabla SE_ATRIBUTOSTAREAS
-------------------------------------------------------------------------------------------------------------
-- para que inicialice CIPOL y pida modo de Autenticaci�n
update se_sist_habilitados set observaciones = 'byxqrZ9hQqI=' where idsistema = 1

IF NOT EXISTS(SELECT * FROM SE_MENSAJES WHERE CODMENSAJE = 4)
BEGIN
	INSERT INTO [SE_MENSAJES] (CODMENSAJE, TEXTOMENSAJE)
	VALUES (4, 'Ya posee una sesi�n activa. Para continuar debe cerrar la sesi�n anterior.')
END

IF NOT EXISTS(SELECT * FROM SE_CODAUDITORIA WHERE CODAUDITORIA = 4)
BEGIN
	INSERT INTO [SE_CODAUDITORIA] (CODAUDITORIA, TEXTOAUDITORIA)
	VALUES (4, 'Intento de Login. Se detecto una sesi�n activa para el usuario @')
END

/*
Para CIPOLWEB es necesario setear bien la PAGINAPORDEFECTO
UPDATE SE_SIST_HABILITADOS
	SET PAGINAPORDEFECTO = 'http://server14/WEBCIPOL/SISTEMASEGURIDAD/frmInicio.aspx'
	WHERE IDSISTEMA = 1

*/
SET NOCOUNT OFF