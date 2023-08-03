ARG VERSION=7.0-alpine
FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env
WORKDIR /src

COPY ["Src/0-Commons/HR.Common.DependencyInjections/",	"0-Commons/HR.Common.DependencyInjections/"]
COPY ["Src/0-Commons/HR.Common.Services/",				"0-Commons/HR.Common.Services/"]
COPY ["Src/0-Commons/HR.Common.Attributes/",			"0-Commons/HR.Common.Attributes/"]
COPY ["Src/0-Commons/HR.Common.Configurations/",		"0-Commons/HR.Common.Configurations/"]
COPY ["Src/0-Commons/HR.Common.DALs/",					"0-Commons/HR.Common.DALs/"]
COPY ["Src/0-Commons/HR.Common.DbContexts/",			"0-Commons/HR.Common.DbContexts/"]
COPY ["Src/0-Commons/HR.Common.Libs/",					"0-Commons/HR.Common.Libs/"]
COPY ["Src/0-Commons/HR.Common.DTOs/",					"0-Commons/HR.Common.DTOs/"]
COPY ["Src/0-Commons/HR.Common.EFCores/",				"0-Commons/HR.Common.EFCores/"]
COPY ["Src/0-Commons/HR.Common.Results/",				"0-Commons/HR.Common.Results/"]
COPY ["Src/0-Commons/HR.Common.Models/",				"0-Commons/HR.Common.Models/"]
COPY ["Src/0-Commons/HR.Common.Enums/",					"0-Commons/HR.Common.Enums/"]
COPY ["Src/0-Commons/HR.Common.Identities/",			"0-Commons/HR.Common.Identities/"]

COPY ["Src/1-Apis/Identities/Identities.Api/",			"1-Apis/Identities/Identities.Api/"]
COPY ["Src/1-Apis/Identities/Identities.CQRS/",			"1-Apis/Identities/Identities.CQRS/"]
COPY ["Src/1-Apis/Identities/Identities.DTOs/",			"1-Apis/Identities/Identities.DTOs/"]
COPY ["Src/1-Apis/Identities/Identities.Services/",		"1-Apis/Identities/Identities.Services/"]

COPY ["scripts/harden.sh"			,"./harden.sh"]
COPY ["scripts/post-install.sh"     ,"./post-install.sh"]
 
RUN dotnet publish "1-Apis/Identities/Identities.Api/Identities.Api.csproj" \
	-c Release \
	-o /app/publish \
	-r alpine-x64 \
	--self-contained true \
	/p:PublishReadyToRun=true \
	/p:PublishReadyToRunShowWarnings=true \
	/p:PublishSingleFile=true \
	/p:TargetLatestRuntimePatch=true

RUN chmod u+x,o+x /app/publish/Identities.Api

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS final

ENV APP_USER=app \
	APP_DIR="/$APP_USER" \
	DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
	DOTNET_RUNNING_IN_CONTAINER=true \
	ASPNETCORE_URLS=http://+:8080

RUN apk add --no-cache icu-libs --upgrade bash && \
	addgroup -S $APP_USER && \
	adduser -S $APP_USER -G $APP_USER

WORKDIR $APP_USER

COPY --from=build-env /src/harden.sh .
RUN chmod +x harden.sh
CMD	"/harden.sh"
RUN	rm harden.sh

COPY --from=build-env --chown=$APP_USER:$APP_USER /$APP_USER/publish .
COPY --from=build-env /src/post-install.sh .

RUN chmod +x post-install.sh
CMD	"/post-install.sh Identities.Api"
RUN	rm post-install.sh

RUN mkdir Files # Create folder 'Files' for write files
RUN chown -R $APP_USER:$APP_USER Files
RUN chmod 755 Files #Set permission to folder 'Files'

USER $APP_USER
EXPOSE 8080
ENTRYPOINT ["./Identities.Api"]