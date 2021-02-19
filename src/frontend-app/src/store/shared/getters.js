export default {
    loading: state => state.loadingCount > 0,
    error: state => state.error,
    success: state => state.success
}
