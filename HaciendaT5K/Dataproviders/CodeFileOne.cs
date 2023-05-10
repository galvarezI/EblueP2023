///
/// this is a sample
///
using Microsoft.SqlServer.Server;


public interface Isqltransaction 
{
    //status

    //commit
    //rollback
}

public class datatransaction
{
    //status

    //commit
    //rollback
}

public class sqltransaction : datatransaction , Isqltransaction
{
    //status

    //commit
    //rollback
}


public class sqlEventArgs : System.EventArgs {

    //status

    //commit
    //rollback

}

public class sqlObject 
{

}

public delegate void SqlTransactionHandler(sqlObject sender, sqlEventArgs e);

//delegate 

public class sqldataprovider 
{

    //private SqlContext context;

    //sql-transaction
    //handler-sql-transaccion
    //on-exec-handler-sql-transaccion
    //on-eval-handler-sql-transaccion

    public sqldataprovider()
    {
        
      
    }

    //exec
    /*
        insert
        delete
        edit
     */


    //eval
    /*
       select
    ---------------------------
       mark-and-doWhat-inWith
     */

}



namespace workspace.z
{

    public class oracleDll
    {

        public bool addRollup(/* cfg, trn, ...?*/) => true;
        /*
         new {cmd = ? , trn =?, params = ? }
         new {cmd = ? , trn =?, params = ? }
         new {cmd = ? , trn =?, params = ? }

         command = new T()
         dadapter = new T() 
         
            if cfg.auto-commit
                trn.commit
         */

    }


}

namespace workspace.y {

    public class campanaDll {




        #region Lote
        public bool addRollupLote(/* trn, args,...?*/) => true;
        /*
            var parameters = args.parameters.SqlBuildParams();
            

            oracleDll.addRollup(...?)
         */
        #endregion

        #region Xentity
        public bool addRollupXentity(/*out handler-transaction ,args,...?*/) => true;
        /*
           var parameters = args.parameters.SqlBuildParams();

           oracleDll.addRollup(...?)
        */
        #endregion
    }


}

namespace workspace.x
{

    public class campanaLib
    {

        /*
          
         */

        public campanaLib()
        {

        }
        public campanaLib(string enterprise, string user, string password)
        {

        }

        #region Lote
        public bool addRollupLote(/*out handler-transaction ,args,...?*/) => true;
        #endregion

        #region Xentity
        public bool addRollupXentity(/*out handler-transaction ,args,...?*/) => true;
        #endregion

    }


}


namespace workspace.w
{

    public class campanaUI
    {

        public void execAddRollupLote() {

            /*
             task.factory.startnew( ()=> {
              this.taskPanel.add (new TaskControl().with( new companyLib (?, ?, ?)))
            } )
             
             */

            if (addRollupLote(/*out trn*/))
            {
                //trn.commit-changes
                
            }
            else {
                //trn.rollback-changes

            }

        }

        public void execAddRollupXentity() { }

        #region Lote
        public bool addRollupLote(/*out handler-transaction ,args,...?*/) => true;
        #endregion

        #region Xentity
        public bool addRollupXentity(/*out handler-transaction ,args,...?*/) => true;
        #endregion


    }


}