
#variables

SCRIPT_FILE=$(readlink -f $0)
WORK_HOME=$(dirname $SCRIPT_FILE)
FX_HOME=$WORK_HOME/../

NUI_HOME=$FX_HOME/src/Tizen.NUI/
#NUI_REF=$NUI_HOME/bin/Release/netstandard2.0/ref/
NUI_BIN=$NUI_HOME/bin/Release/netstandard2.0/*

FLUX_HOME=$FX_HOME/src/Tizen.NUI.FLUX/
#FLUX_REF=$FLUX_HOME/bin/Release/netstandard2.0/ref/
FLUX_BIN=$FLUX_HOME/bin/Release/netstandard2.0/*

WORK_BIN=$WORK_HOME/bin/
INPUT_HOME=$WORK_BIN/input/
INPUT_REF=$INPUT_HOME/ref/
OUTPUT_HOME=$WORK_BIN/output/

#rm -rf $WORK_BIN
#mkdir -p $WORK_BIN

#dotnet publish $TOOL_HOME -o $WORK_BIN
#$WORK_HOME/update-tools.sh

rm -rf $INPUT_HOME
mkdir -m 777 -p $INPUT_REF

#dotnet build $NUI_HOME -c Release /fl
#dotnet build $FLUX_HOME -c Release /fl

cp -rf $NUI_BIN $INPUT_HOME
cp -rf $FLUX_BIN $INPUT_HOME

rm -rf $OUTPUT_HOME
mkdir -m 777 -p $OUTPUT_HOME

CACHE=`pwd`
cd $OUTPUT_HOME
dotnet $WORK_BIN/APITool.dll dummy $INPUT_REF $OUTPUT_HOME
cd $CACHE

