

Visual Studio .Net 

IDE
	Options

		Windows Form Designers		
		   . <hash> thisGeneral	// this is assigned
				<symbol:nativeFORvalues>
		   . <hashMatrix> thisHashs
		   . <secuencyHashList> thisSecuencyList
		   . (hashTtype.x) symbol:nativeFORvalues, Native Values
		   . (hashTtype.y) symbol:uiFORrenderization, UI renderization
   


flow
   signUp
	 validateFORform, 
	 fillFORform, 
	 sendFORmail,
	 attendFORconfirmation|unnattendFORconfirmation with AUTH
		emailFORlink| emailFORcode

   signIn
	 validateFORform, 
	 fillFORform, 
	 sendFORmail,
	 attendFORauthentication|unnattendFORauthentication with AUTH
		emailFORlink| emailFORcode



...activity
  ...task

assembly

process : assembly, &assembly.limited, Iserviceable > start, stop, pause, resume, reanude, ...method

subprocess(:process expProcess)=> exp.Process.bindFor(subpro: this) => this.subpromatrix.quequeAdd(subpro, onDispose: exp => exp.callDispose)
	-> like 
		webworker
		: work bindable.independente(simultaneo)

namespace enmark
	...namespaces
	#region
	#region interfaceFORstruct (native model ), (finalizer region)
	interface IbaseOn<blop>
	interface facet
	interface facetFORstruct<blop> {
	
	}

namespace engage
	namespace corlib
	  
		namespace montanosoft
		
		namespace factory {
			#region
			#region struct (native data ), (finalizer region)
		 }

	namespace

at.modelingFORliveFORscope

static class modelingFORliveFORscope<...?, uniqueidentifier>
where uniqueidentifier: struct, 

at.namingFORliveFORscope

static class namingFORliveFORscope<...?, ?>

public struct keydataPair<key, data> 
{
	<key>  id    {get;set;}
	<data> value {get;set;} 

}

at.defineFORliveFORscope

static class defineFORliveFORscope<...?, keyvaluePair>
where keyvaluePair: struct

agent

public interface IextendedOn<T, C>
public interface IstrongFORcollection<>

public abstract class basedOn<T>
where T: class, new()
{


}

agentJob
	.<job> thisJob
	.<schedule> thisSchedule
	.<stepMatrix> thisSteps
	.<secuencyStepMatrix> thisSecuencySteps

stepMatrix
	.<...step>
	.<dictionary<?(...?), step>>

secuencyStepMatrix
	.<...secuencyStep>
	.<dictionary<?(...?), secuencyStep>>

secuencyStepCollection: basedOn<secuencyStepMatrix>


secuencyStep
	.<agentJob>
	.<step>
	.type : initializer, first, start, ...?,  end, last finalizer


job
 ...step

step
	.<>
	agent > agent[1].job("job ?")
	readobly myJob => agent.thisJob;
	name
	command
	commandType : procedure, sqlcommand, sqlsetting, sqlcommand-batch, sqlsetting-batch
	
	

agent<T:job> where T: Idescriptible<forProperties>: agent, Ivisualizable, Isettingnable, Iappoinmentable
 {
   properties > from T.job
  }


use cases modelFORtemplate
1 prepare breakfast

syncronos
prepare breakfast
1:first
2:w.?.timestamp; start
3:x.?.timestamp
.
.
7:y.?.timestamp
8:z.?.timestampend

9:last
elapseDT = w.? + <x,y,z>.? -->
like 45min

asyncronos
A
1:first
2:w.?.timestamp; start
3:x.?.timestamp
.
.
7:y.?.timestamp
8:z.?.timestampend

9:last
elapseDT = w.? + <x,y,z>.?
factorOne = 1m / 3m 		//m.decimal => 0.333333333333333m
factorTwo = 86.4m / 120m	//m.decimal => 0.72m | 0.72f | 0.72d

like 
	from(45min) > 42.75min >> 30.78min 
	>: evalStats(<int>capacity: [1 from 9] with 1 to ? expOne, <elapseTimeStamp> elapseDT: expTwo ) 
	  (len, value, x.factor, y.factor)=> $(reallen.size: (x.factor * len) + len) => $(realvalue.amount: reallen + value) 
	  => $(x: amount/size ) => (y: len * x ) => (z: y.factor * y) => return z: new {sync:45min, esforce:42.75min  ,async:30.78min }


B
1:first
2:w.?.timestamp; start
3:x.?.timestamp
.
.
7:y.?.timestamp
8:z.?.timestampend

9:last
elapseDT = w.? + <x,y,z>.?
factorOne = 1m / 1.5m 		//m.decimal => 0.666666666666667m
factorTwo = 28.8m / 120m	//m.decimal => 0.24m | 0.24f | 0.24d

like 
	from(45min) > 36.00min >> 8.64min 
	>: evalStats(<int>capacity: [1 from 9] with 1 to ? expOne, <elapseTimeStamp> elapseDT: expTwo ) 
	  (len, value, factor)=> $(reallen: (factor * len) + len) => $(realvalue: reallen + value) 
	  => $(x: amount/size ) => (y: len * x ) => (z: y.factor * y) => return z: new {sync:45min, esforce:36.00min, async:8.64min }

0.333333333333333[1,15] nop 0 changes
0.666666666666667[1,15] yes 1 changes :at last

0.72[1,2] yes * changes
0.24[1,2] yes * changes