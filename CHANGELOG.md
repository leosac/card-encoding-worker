# Leosac Credential Provisioning Encoding Worker - Changelog
## v1.22.0
### Changed
- Add CheckFieldValueMaxLength property on access control service
- Skip NFCApplicationKey parameter on WriteSDMFile action if KeyId is empty

## v1.21.0
### Changed
- Add optional NFCApplicationKey property on WriteSDMFile
- Fix potential segfault on InitializeKeySet DESFire command

## v1.20.0
### Changed
- Remove unnecessary key authentication on Write SDM helper command
- Fix SSCPv2 reader communication

## v1.19.2
### Changed
- Fix wrong MAC calculation on DESFire EV1 full communication mode response

## v1.19.0
### Added
- Support for STid Biometric readers on Prepare Biometric Data Service

### Changed
- DESFire EV1 legacy random UID detection on cardprobe
- Global option to use system readers (for PC/SC only atm)
- Avoid default use of DESFire ISO Authenticate command
- Fix reference to SAM chip and check for old key declaration on DESFire change key

## v1.18.1
### Added
- Options to configure a global PKCS#11 library
- Handle SAM Authenticate Host option
- New parameter to force the SAM type

## v1.17.0
### Added
- NDEF related commands and service
- FromField property on NdefRecord
- DESFire EV2 NXP ECDSA Originality Check
- DESFire EV3 options for SDM