using System;


namespace Eblue.Code
{
    [Flags()]
    public enum RolePlayerType:byte
    {
        None,
        Vtype,
        Wtype,
        Xtype = 4,
        Ytype = 8,
        //YtypeAll = 9,
        //YtypeZs = 15,
        Ztype = 16
    }

//Vtype player [] {work.member, task.officer}			can edited when prepared, nop apply<aprove, reject>
//Wtype player [] {directive.leader}				can prepared, edited, nop apply<aprove, reject>
//Xtype player [] {visor.company, work.administrator} 		can noted when revised|evaluated , nop apply<aprove, reject>
//Ytype player [] {assistant.leader} 				can noted , yes apply<aprove, reject>
//Ztype player [] {investigation.officer, directive.manager}	can create project and noted, revised, evaluated, amend, yes apply<aprove, reject, exec>
}